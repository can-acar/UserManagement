namespace UserManagement.Infrastructure.Commons
{
    public class ServiceResponse
    {
        public object Data { get; set; }

        public object Errors { get; set; }

        public string Message { get; set; }

        public bool Status { get; set; }


        public static async Task<ServiceResponse> SuccessAsync(string message = null)
        {
            return await Task.FromResult(new ServiceResponse
            {
                Status = true,
                Message = message
            });
        }


        public static async Task<ServiceResponse> SuccessAsync(string message, object data)
        {
            return await Task.FromResult(new ServiceResponse
            {
                Status = true,
                Message = message,
                Data = data
            });
        }

        public static async Task<ServiceResponse> ErrorAsync(object error, string message = null)
        {
            return await Task.FromResult(new ServiceResponse
            {
                Status = false,
                Message = message,
                Errors = error
            });
        }

        public static async Task<ServiceResponse> ErrorAsync(string message = null)
        {
            return await Task.FromResult(new ServiceResponse
            {
                Status = false,
                Message = message
            });
        }

        // generate without task
        public static ServiceResponse Success(string message = null)
        {
            return new ServiceResponse
            {
                Status = true,
                Message = message
            };
        }

        public static ServiceResponse Success(string message, bool status, object data)
        {
            return new ServiceResponse
            {
                Status = status,
                Message = message,
                Data = data
            };
        }

        public static ServiceResponse Error(object error, string message = null)
        {
            return new ServiceResponse
            {
                Status = false,
                Message = message,
                Errors = error
            };
        }


        public static ServiceResponse Error(string message = null)
        {
            return new ServiceResponse
            {
                Status = false,
                Message = message
            };
        }
    }
}