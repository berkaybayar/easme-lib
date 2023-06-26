# EasMe.Box .NET Framework 4.8
Provides a simple way to create error, warning, information message boxes in .NET Framework 4.8

### Methods
```csharp
//Shows message box 
EasMe.Box.Show("Message");

//Show error message box
EasMe.Box.Error("Error message");

//Show warning message box
EasMe.Box.Warning("Warning message");

//Show information message box
EasMe.Box.Information("Information message");

//Show message box for confirmation and returns true if user clicks "Yes" button
var result = EasMe.Box.Confirm("Are you sure you want to continute ?");

//Shows message box with Stop icon
EasMe.Box.Stop("You are not allowed");
```