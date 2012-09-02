# Validator.NET - A validator for XSS and SQL injections attacks, validates phone numbers, zip codes and more.

### Features

* Detects XSS attacks by using a probability factor.
* Detects SQL injection attacks by using a probability factor.
* Support for normalizing data input by decoding HEX, Unicode and removing newlines.
* Support for common input checks such as E-mails, URI’s, phone number and others.
* Support for numerous different phone number, postal code and social security number formats. Like UK, Brazil, Spain and United States.
* Support for common range, type and length checks.

### Examples

```csharp
static void Main(string[] args)
{
	int attackVector = Validator.CheckForXss("<ScRiPt lAnguage=JavaScript> alert(document.cookie); var hextest=%74%65%73%74; &lt;/sCrIpt&gt;");

	if (attackVector > 500)
	{
		// Take the appropriate action.
	}
}
```

Output:
```

```

For more examples, take a look at the Validator.NET Unit tests included in the project.