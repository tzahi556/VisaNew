<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
   <meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
		<title>Selectize.js Demo</title>
		<meta name="description" content="">
		<meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1">


        <link rel="stylesheet" href="css/normalize.css">
		<%--<link rel="stylesheet" href="css/stylesheet.css">--%>
		<!--[if IE 8]><script src="js/es5.js"></script><![endif]-->
		<script src="js/jquery.min.js"></script>
		<script src="../dist/js/standalone/selectize.js"></script>
		<script src="js/index.js"></script>

		<!--[if IE 8]><script src="js/es5.js"></script><![endif]-->
		
	


              
                <script>




                    // A $( document ).ready() block.
                    $(document).ready(function () {

                        // $("#select-repeated-options option[value='a2']").text()
                        // var res =  $("#select-repeated-options option[value='a2']");
                     
                        buildKaka();
                    });



                    function buildKaka() {

                        $('#select-repeated-options').selectize({

                            render: {
                                //				            optgroup_header: function (data, escape) {
                                //				                return '<div class="optgroup-header" style="font-size:15px">' + escape(data.label) +'</div>';
                                //				            },


                                option: function (item, escape) {

                                    if (item.value.indexOf("_")!="-1") {
                                        return '<div class="optgroup-header" style="font-size:15px">' + item.text + '</div>';
                                    }
                                    else
                                        return '<div style="margin-left:10px">' + item.text + '</div>';

                                }


                            }
                        });


                    }


				</script>



</head>
<body>
    <form id="form1" runat="server">
   		<div class="demo" style="padding:20px;">
		<h2>Optgroups (repeated options)</h2>

     
        
        
            
				<div class="control-group">
				
					<select id="select-repeated-options" class="demo-default" >
						<option value="">Select...</option>
                    	<option value="a2_2">Item A</option>
					    <option value="b2">Item B</option>
                    
					</select>
				</div>
				
                
          
			</div>
    </form>
</body>
</html>
