////using ApplicationCore.Contract;
////using Infrastructure.Services;

////namespace InventoryManagementSystem.CustomMiddleware
////{
////    public class JwtValidationMiddleware

////    {
////        private readonly RequestDelegate _next;
////        private readonly IJwtTokenServices _jwtService;

////        public JwtValidationMiddleware(RequestDelegate next, IJwtTokenServices jwtService)
////        {
////            _next = next;
////            _jwtService = jwtService;
////        }

////        public async Task InvokeAsync(HttpContext context)
////         {
////            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

////            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
////            {
////                context.Response.StatusCode = 401;
////                await context.Response.WriteAsync("Token is missing or invalid.");
////                return;
////            }

////            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
////            var user = _jwtService.ValidateToken(token);

////            if (user == null)
////            {
////                context.Response.StatusCode = 401;
////                await context.Response.WriteAsync("Invalid token.");
////                return;
////            }

////            // Token is valid, proceed to the next middleware
////            await _next(context);
////        }
////    }
////}
//using ApplicationCore.Contract;
//using Infrastructure.Services;

//namespace InventoryManagementSystem.CustomMiddleware
//{
//    public class JwtValidationMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly IJwtTokenServices _jwtService;

//        public JwtValidationMiddleware(RequestDelegate next, IJwtTokenServices jwtService)
//        {
//            _next = next;
//            _jwtService = jwtService;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            // Check if the request is targeting an API route (i.e., path starts with "api/")
//            if (context.Request.Path.StartsWithSegments("/api"))
//            {
//                var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            
//                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
//                {
//                    context.Response.StatusCode = 401;
//                    await context.Response.WriteAsync("Token is missing or invalid.");
//                    return;
//                }

//                var token = authorizationHeader.Substring("Bearer ".Length).Trim();

 
//                var user = _jwtService.ValidateToken(token);

//                if (user == null)
//                {
//                    context.Response.StatusCode = 401;
//                    await context.Response.WriteAsync("Invalid token.");
//                    return;
//                }
//            }

//            // Proceed to the next middleware
//            await _next(context);
//        }
//    }
//}
