WPF Exception MessageBox
===================

The WPF Exception MessageBox is a custom gui MessageBox specifically designed to display the details
of an Exception object to the user for debugging purposes that targets the Microsoft Windows Presentation
Framework (WPF).

This project is currently available for Framework 4.7.2 and 4.8, Core 3.1 and NET 5 and 6.

To use the WPF Exception MessageBox, simply put this in a catch statement:

```csharp
//...
using WpfExceptionMessageBox;
//...
    try
    {
        // Do something wrong here
    }
    catch (Exception e)
    {
        // TO quickly display the exception, just pass
        // the exception to the static show method.
        _ = ExceptionMessageBox.Show(e)
    }
```

The `ExceptionMessageBox.Show` static method has multiple overloads that allows customization of the
message box title and severity icon that is displayed in the box.

Default Display:

![Default Display](https://github.com/rmboggs/ExceptionMessageBox/blob/main/Documentation/Images/Wpf/Default.PNG)

Display with Informational severity:

![Display with Informational severity](https://github.com/rmboggs/ExceptionMessageBox/blob/main/Documentation/Images/Wpf/Informational.PNG)

Display with Warning severity:

![Display with Warning severity](https://github.com/rmboggs/ExceptionMessageBox/blob/main/Documentation/Images/Wpf/Warning.PNG)

Additional Exception Details Window:

![Additional Exception Details Window](https://github.com/rmboggs/ExceptionMessageBox/blob/main/Documentation/Images/Wpf/AdditionalExceptionDetails.PNG)

You can also provide additional help to the user by adding a link to additional help details to the thrown exception:

```csharp
            try
            {
                throw new OutOfMemoryException();
            }
            catch (Exception e)
            {
                e.HelpLink = "https://dotnet.microsoft.com/";
                _ = ExceptionMessageBox.Show(owner, e, ExceptionSeverity.Warning);
            }
```

Additional Exception Details Window with Help button:

![Additional Exception Details Window with Help button](https://github.com/rmboggs/ExceptionMessageBox/blob/main/Documentation/Images/Wpf/AdditionalExceptionDetailsWithHelp.PNG)

The exception details can be copied from the message box so that they can be easily saved or emailed
to someone else for additional analysis.

This is how the exception details will look when copied from the exception box. This example is
for a thrown AggregateException object, which the message box will capture all inner exception
details as well as the parent exception:

```
Exception Type: AggregateException
=======================

Exception Message:
One or more errors occurred. (Invalid cast occurred, in theory) (Operation is not valid due to the current state of the object.)

Exception Stacktrace:
   at System.Threading.Tasks.Task.WaitAllCore(Task[] tasks, Int32 millisecondsTimeout, CancellationToken cancellationToken)
   at System.Threading.Tasks.Task.WaitAll(Task[] tasks)
   at EmbWpfExample.MainWindowViewModel.Aggregated(Window owner) in ...\exceptionmessagebox\Samples\EmbWpfExample\MainWindowViewModel.cs:line 80

Target Site:
ThrowAggregateException

Exception Source:
System.Private.CoreLib

***********************

Exception Type: InvalidCastException
=======================

Exception Message:
Invalid cast occurred, in theory

Exception Stacktrace:
   at EmbWpfExample.MainWindowViewModel.<>c.<<Aggregated>b__19_0>d.MoveNext() in ...\exceptionmessagebox\Samples\EmbWpfExample\MainWindowViewModel.cs:line 73

Target Site:
MoveNext

Exception Source:
EmbWpfExample


Exception Type: InvalidOperationException
=======================

Exception Message:
Operation is not valid due to the current state of the object.

Exception Stacktrace:
   at EmbWpfExample.MainWindowViewModel.<>c.<<Aggregated>b__19_1>d.MoveNext() in ...\exceptionmessagebox\Samples\EmbWpfExample\MainWindowViewModel.cs:line 78

Target Site:
MoveNext

Exception Source:
EmbWpfExample
```
