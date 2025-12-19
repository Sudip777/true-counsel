// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;
[assembly: SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ASP.NET Core DI")]
[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>", Scope = "member", Target = "~M:ProblemDetailsExceptionMiddleware.InvokeAsync(Microsoft.AspNetCore.Http.HttpContext)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Performance", "CA1869:Cache and reuse 'JsonSerializerOptions' instances", Justification = "<Pending>", Scope = "member", Target = "~M:ProblemDetailsExceptionMiddleware.HandleExceptionAsync(Microsoft.AspNetCore.Http.HttpContext,System.Exception)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "<Pending>", Scope = "type", Target = "~T:TrueCounsel.API.Controllers.AuthController.AuthController")]
[assembly: SuppressMessage("Major Code Smell", "S6960:Controllers should not have mixed responsibilities", Justification = "<Pending>", Scope = "type", Target = "~T:TrueCounsel.API.Controllers.AuthController.AuthController")]
[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>", Scope = "member", Target = "~M:TrueCounsel.API.Middlewares.ProblemDetailsExceptionMiddleware.InvokeAsync(Microsoft.AspNetCore.Http.HttpContext)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Critical Code Smell", "S4487:Unread \"private\" fields should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:TrueCounsel.API.Middlewares.ProblemDetailsExceptionMiddleware._logger")]
[assembly: SuppressMessage("Performance", "CA1869:Cache and reuse 'JsonSerializerOptions' instances", Justification = "<Pending>", Scope = "member", Target = "~M:TrueCounsel.API.Middlewares.ProblemDetailsExceptionMiddleware.HandleExceptionAsync(Microsoft.AspNetCore.Http.HttpContext,System.Exception)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Major Code Smell", "S6966:Awaitable method should be used", Justification = "<Pending>")]
