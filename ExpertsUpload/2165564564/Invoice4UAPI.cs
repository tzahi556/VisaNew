using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class Invoice4UAPI
{

    public static string CreateBlacarDocumentRefund(string NameForInvoice, string email, double amount, string subject, string rowText, IConfiguration configuration,string invoice)
    {

        CustomerObj cust = new CustomerObj(configuration);
        var CurrentCustomer = cust.GetOrCreateCustomer(NameForInvoice, email);




        CreateInvoiceReceipt d = new CreateInvoiceReceipt(configuration);

        // var NewDocumnet = d.CreateDocumentGeneralClient(CurrentCustomer, amount, subject, rowText, email, NameForInvoice);
        var NewDocumnet = d.CreateDocumentRefund(CurrentCustomer, amount, subject, rowText, email, NameForInvoice,Helper.ConvertToInt(invoice));



        return NewDocumnet.DocumentNumber.ToString();

    }

    public static string CreateBlacarDocument(string NameForInvoice, string email, double amount, string subject, string rowText, IConfiguration configuration)
    {

        CustomerObj cust = new CustomerObj(configuration);
        var CurrentCustomer = cust.GetOrCreateCustomer(NameForInvoice, email);




        CreateInvoiceReceipt d = new CreateInvoiceReceipt(configuration);

        var NewDocumnet = d.CreateDocumentGeneralClient(CurrentCustomer, amount, subject, rowText, email, NameForInvoice);



        return NewDocumnet.DocumentNumber.ToString();

    }




}


public class LoginFunctions
{


    public string email = "Test@test.com";//"datacheck.invoice@gmail.com";
    public string password = "123456";//"3Desalto3";

    public LoginFunctions(IConfiguration _configuration)
    {
        email = _configuration.GetValue<string>("Invoice4U:UserName");
        password = _configuration.GetValue<string>("Invoice4U:Password");

    }



    ApiServiceClient apiSrv = new
    ApiServiceClient(ApiServiceClient.EndpointConfiguration.BasicHttpBinding_ApiService1);


    public string GetToken()
    {
        return apiSrv.VerifyLogin(email, password);
    }

    public User isAuthenticated(string token)
    {
        return apiSrv.IsAuthenticated(token);
    }

}

public class ServiceInstance
{
    public ApiServiceClient apiSrv = new
    ApiServiceClient(ApiServiceClient.EndpointConfiguration.BasicHttpBinding_ApiService);

    public void ResetService()
    {
        apiSrv = new
        ApiServiceClient(ApiServiceClient.EndpointConfiguration.BasicHttpBinding_ApiService);
    }
}

public class CustomerObj : ServiceInstance
{


    public IConfiguration _configuration;
    public string token;


    public CustomerObj(IConfiguration configuration)
    {
        _configuration = configuration;
        token = new LoginFunctions(_configuration).GetToken();

    }

    public Customer GetCustomers()
    {
        Customer customer = new Customer();
        var cust = apiSrv.GetCustomers(customer, token);
        return cust.Response[4];

    }

    /* Function for Customer Create detail*/
    //public Customer CreateCustomer()
    //{





    //    User User = new LoginFunctions().isAuthenticated(token);
    //    Customer customer = new Customer()
    //    {
    //        Name = "צחיאל חזן",
    //        Email = "tzahi@gmail.com",
    //        Phone = "0465422356",
    //        Fax = "046588689",
    //        Address = "מעגלים הערבה",
    //        City = "נתיבות",
    //        Zip = "523367",
    //        UniqueID = "064558083",
    //        OrgID = User.OrganizationID,
    //        PayTerms = 30,
    //        Cell = "0505913817",
    //        Active = true,

    //    };

    //    customer = apiSrv.CreateCustomer(customer, token);

    //    if (customer.Errors.Length > 0)
    //    {
    //        // HANDLE ERROR
    //    }
    //    else
    //    {
    //        // HANDLE SUCCESS
    //    }
    //    return customer;
    //}


    public Customer GetOrCreateCustomer(string name, string email)
    {
        var res =
        apiSrv.GetCustomers(new Customer()
        { Name = name, Email = email, Active = true }, token);
        // customer found - return
        if (res != null && res.Response != null
           && res.Response.Count() > 0)
        {
            return res.Response[0];
        }
        //  customer dosen't exists - create customer
        else
        {
            return apiSrv.CreateCustomer(new Customer()
            { Name = name, Email = email }, token);
        }
    }

    public Customer Customer()
    {
        //BasicHttpBinding binding = new BasicHttpBinding();

        //// Use double the default value
        //binding.MaxReceivedMessageSize = 65536 * 2;


        Customer customer = new Customer();

        var cust = apiSrv.GetCustomersByOrgId(token);
        return cust.Response[11];

    }

}

class CreateInvoiceReceipt : ServiceInstance
{


    public IConfiguration _configuration;
    public string token;


    public CreateInvoiceReceipt(IConfiguration configuration)
    {
        _configuration = configuration;
        token = new LoginFunctions(_configuration).GetToken();

    }


    // string token = new LoginFunctions(_configuration).GetToken();

    // InvoiceReceipt for general client

    public Document CreateDocumentGeneralClient(Customer cust, double amount, string subject, string desc, string name, string idn)
    {
        Document doc = new Document()
        {
            GeneralCustomer = new GenerelCustomer()
            {
                Name = name,

                Identifier = idn
            },
            // ClientID = cust.ID,//CustomerObj. GetOrCreateCustomer("Rohit").ID,

            Currency = "ILS",

            // calculate the tax backwards , for example ,
            //if your price input is 100 , this would be the total
            //    sum and the tax would be -100 / 1.17
            TaxIncluded = true,

            DocumentType = (int)DocumentType.InvoiceReceipt,
            Items = new DocumentItem[]
            {
                new DocumentItem(){
                    Code = "", // catalog item code
                    Name = desc,
                    Price = amount,
                    Quantity = 1
                }
            },
            Payments = new Payment[]{
                        new Payment(){
                            Date=DateTime.Now,
                            Amount=amount,
                            PaymentType= (int)PaymentTypes.CreditCard


                        },

            },
            // you can round the total items sum up to 0.5
            RoundAmount = 0,
            Subject = subject,
            TaxPercentage = (_configuration.GetValue<float>("Vat") * 100),
            AssociatedEmails = new AssociatedEmail[]
            {
                // send info mail to user account mail
                new AssociatedEmail()
                {
                    Mail = _configuration.GetValue<string>("Invoice4U:InfoMail"),
                    IsUserMail = true
                },
                // send mail to customer
                new AssociatedEmail()
                {
                    Mail = cust.Email,
                    IsUserMail = false
                }
            },
            // You can add your own guide 
            //to track the documents later on your side
            //ApiIdentifier = Guid.NewGuid().ToString()
        };

        doc = apiSrv.CreateDocument(doc, token);

        if (doc.Errors.Length > 0)
        {
            // HANDLE ERROR
        }
        else
        {
            // HANDLE SUCCESS
        }
        return doc;
    }

    public Document CreateDocumentRefund(Customer cust, double amount, string subject, string desc, string name, string idn,int invoice)
    {

        Document doc = new Document()
        {
            //using for InvoiceCredit

            GeneralCustomer = new GenerelCustomer()
            {
                Name = name,

                Identifier = idn
            },

            //ClientID = GetOrCreateCustomer("Rohit").ID,
            DocumentType = (int)DocumentType.InvoiceCredit,
            Subject = "Document Subject",
            Currency = "ILS",
            // can be at the Past no erlierthen last invoice
            IssueDate = DateTime.Now,
            Total = amount,
            CreditAmount = amount,
            Invoices = GetInvoices
            (token, amount, DocumentType.InvoiceCredit,invoice),
            DocumentReffType = (int)DocumentType.InvoiceReceipt,

            AssociatedEmails = new AssociatedEmail[]
            {
                 // send info mail to user account mail
                new AssociatedEmail()
                {
                    Mail = _configuration.GetValue<string>("Invoice4U:InfoMail"),
                    IsUserMail = true
                },
                // send mail to customer
                new AssociatedEmail()
                {
                    Mail = cust.Email,
                    IsUserMail = false
                }
            },


        };

        doc = apiSrv.CreateDocument(doc, token);

        if (doc.Errors.Length > 0)
        {
            // HANDLE ERROR
        }
        else
        {
            // HANDLE SUCCESS
        }
        return doc;


    }

    private Document[] GetInvoices(string token, double sumToCredit, DocumentType documentType,int invoice)
    {
        // Init array
        Document[] docs = new Document[1];
        //Get document to credit
        DocumentsRequest dr = new
        DocumentsRequest()
        {
            DocumentNumber = invoice,
            Type = DocumentType.InvoiceReceipt,
            ReportType = ReportTypes.Document
        };
        docs[0] = apiSrv.GetDocuments(dr, token).Response[0];
        //Set Credit Amount
        if (documentType == DocumentType.InvoiceCredit)
            docs[0].CreditAmount = sumToCredit;
        docs[0].ReceiptAmount = sumToCredit;
        return docs;
    }

}
/*  PaymentTypes enum*/
public enum PaymentTypes
{
    CreditCard = 1,
    Check = 2,
    MoneyTransfer = 3,
    Cash = 4,
    Credit = 5,
    WithholdingTax = 6

}





