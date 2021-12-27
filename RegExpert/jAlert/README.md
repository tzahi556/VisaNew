# jAlerts
A light-weight customizable JavaScript library to replace the browser's default alerts. [View Demos](http://www.jsangerdevelopment.com/jAlerts/index.php)
## Basic Implementation
**1**.   Link jAlerts CSS File
```
<link rel="stylesheet" type="text/css" href="./path-to-css/jAlerts.css">
```
**2**.   Link to jQuery v1.9+ (If not using already) and jAlerts.min.js files.
```
<script src="./path-to-js/jquery.min.js"></script>
<script src="./path-to-js/jAlerts.min.js"></script>
```
**3**.   Call jAlerts function. [View documentation](http://www.jsangerdevelopment.com/jAlerts/overview.php)
```javascript
// Notice the properties are not in quotes, only the values being entered need quotes
jAlert ( {

	headingText		:	"Attention!"	,  // Important 
	contentText		:	"Both the heading and content text have now been changed"	
		
}, "top" ) ;
```
