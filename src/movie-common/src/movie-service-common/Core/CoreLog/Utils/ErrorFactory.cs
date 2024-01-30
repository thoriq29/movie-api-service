using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;
using Service.Core.Log.Errors;

namespace Service.Core.Log.Utils
{
    public class ErrorFactory : IErrorFactory
    {
        public IErrorException CreateErrorException(IError error)
        {
            return new ErrorException(error);
        }

        public IError CreateError(string code, string message, string target, object[] details, IInnerError innererror)
        {
            return new Error()
            {
                code = code,
                message = message,
                target = target,
                details = details,
                innererror = innererror
            };
        }
        
        public IError CreateError(string code, string message)
        {
            return new Error()
            {
                code = code,
                message = message
            };
        }

        /// 1000XX - Template

        // 10000X - Service Error Related
        public IError CreateIdNotValid()
        {
            return new Error() { code = "100000", message = "Id is not valid" };
        }
        public IError CreateFileNotValid()
        {
            return new Error() { code = "100001", message = "File uploaded is not valid" };
        }
        public IError CreateParameterNotValid()
        {
            return new Error() { code = "100002", message = "Parameter is not valid" };
        }
        public IError CreateMalformedData()
        {
            return new Error() { code = "100003", message = "Malformed Data" };
        }
        public IError CreateUpdateDataFailed()
        {
            return new Error() { code = "100004", message = "Data Update in DB has failed" };
        }

        // 10001X - User Related
        public IError CreateUserWithSameNickname()
        {
            return new Error() { code = "100010", message = "User with same nickname has already exist" };
        }
        public IError CreateUserAlreadyBindedToAnotherAccount()
        {
            return new Error { code = "100011", message = "User already binded to another account" };
        }
        public IError CreateAccessTokenMalformed()
        {
            return new Error { code = "100012", message = "Access token malformed on cookie" };
        }
        public IError CreateIDAlreadyExists()
        {
            return new Error() { code = "100013", message = "Id of the model in the message already exists in the database" };
        }
        public IError CreateIDNotFound()
        {
            return new Error() { code = "100014", message = "Id of the model in the message doesn't exist in the database" };
        }
        public IError CreateFailedLoginAPI()
        {
            return new Error() { code = "100015", message = "Failed login to API" };
        }

        // 10002X - 10003X - IAP Purchase
        public IError CreateNotAuthorized()
        {
            return new Error() { code = "100021", message = "Not Authorized to access this API." };
        }
        public IError CreatePurchaseDataNotFound()
        {
            return new Error() { code = "100022", message = "Purchase data cannot be found." };
        }
        public IError CreatePurchaseDataCancelled()
        {
            return new Error() { code = "100023", message = "Purchase has been cancelled." };
        }
        public IError CreateRetryableReceipt()
        {
            return new Error() { code = "100024", message = "Failed receipt that retryable." };
        }
        public IError CreateNotRetryableReceipt()
        {
            return new Error() { code = "100025", message = "Failed receipt that not retryable." };
        }
        public IError CreateProviderNativeError()
        {
            return new Error() { code = "100026", message = "Error from provider." };
        }
        public IError CreateEmptyContent()
        {
            return new Error() { code = "100027", message = "Content is empty." };
        }
        public IError CreatePayloadNotParse()
        {
            return new Error() { code = "100028", message = "Payload cannot be parsed." };
        }
        public IError CreateUnknownPlatform()
        {
            return new Error() { code = "100029", message = "Store platform cannot be recognized" };
        }
        public IError CreateIAPProductNotFound()
        {
            return new Error() { code = "100030", message = "IAP Product cannot be found" };
        }
        public IError CreateUnknownError()
        {
            return new Error() { code = "100031", message = "Unknown Error occurred." };
        }

        // 10004X - Filter
        public IError CreateWhereQueryOperatorInvalid()
        {
            return new Error() { code = "100040", message = "Where query operator invalid. Please use ( '=' | 'contains' | '>' | '>=' | '<' | '<=' )" };
        }
        public IError CreateWhereQueryValueTypeInvalid()
        {
            return new Error() { code = "100041", message = "Where Query Value Type Invalid. Mostly happened because you have wrong data types. Ex Type DateTime but with Operator = contains" };
        }
        public IError CreateWhereQueryKeyInvalid()
        {
            return new Error() { code = "100042", message = "Where query key invalid." };
        }
        public IError CreateWhereQueryValueInvalid()
        {
            return new Error() { code = "100043", message = "Where query value invalid, Not match with the key type." };
        }
        public IError CreateOrderQuerySortInvalid()
        {
            return new Error() { code = "100044", message = "Order query sort invalid. Please use ( 'asc' | 'desc' )" };
        }
        public IError CreateOrderQueryKeyInvalid()
        {
            return new Error() { code = "100045", message = "Order query key invalid." };
        }
        public IError CreateLimitQueryValueInvalid()
        {
            return new Error() { code = "100046", message = "Limit query value invalid. Bro dont use negative number or end value is bigger then start value :(" };
        }

        // 10005X - 10006X - Auth
        public IError CreateAuthUserNotMatch()
        {
            return new Error() { code = "100050", message = "User not match" };
        }
        public IError CreateAuthTokenUserInvalid()
        {
            return new Error() { code = "100051", message = "Auth token user invalid" };
        }
        public IError CreateAuthUserNotFound()
        {
            return new Error() { code = "100052", message = "Auth user not found" };
        }
        public IError CreateLoginInvalid()
        {
            return new Error() { code = "100053", message = "Login Credentials is not found or invalid" };
        }
        public IError CreateUserAlreadyExist()
        {
            return new Error() { code = "100054", message = "User already exist" };
        }
        public IError CreateUserNotFound()
        {
            return new Error() { code = "100055", message = "User not found" };
        }
        public IError CreateAccountIsTempDisabled()
        {
            return new Error() { code = "100056", message = "Account is Temporary Disabled" };
        }
        public IError CreateAuthTokenInvalid()
        {
            return new Error { code = "100057", message = "Authorization Token Invalid" };
        }
        public IError CreateInsufficientParams()
        {
            return new Error() { code = "100058", message = "Insufficient Parameter" };
        }
        public IError CreateInvalidOperation()
        {
            return new Error { code = "100059", message = "Invalid Operation" };
        }
        public IError CreateInvalidRequestDetected()
        {
            return new Error { code = "100060", message = "Invalid Operation" };
        }
        public IError CreatePlayerIDNotFound()
        {
            return new Error() { code = "100061", message = "Player ID Not Found" };
        }

        // 10006X - Redis Related
        public IError CreateNoExistingDistributedCache()
        {
            return new Error() { code = "100062", message = "No existing distributed cache, cannot cache some string value" };
        }

        // 10007X - Voucher
        public IError CreateFailedToProcessVoucherRequest()
        {
            return new Error() { code = "100070", message = "Failed to process voucher request on Gems Service" };
        }
        public IError CreateInvalidVoucherData()
        {
            return new Error() { code = "100071", message = "Invalid voucher data" };
        }

        // 10008X - Internal Server
        public IError CreateContentNotFound()
        {
            return new Error() { code = "100080", message = "Content Not Found" };
        }
        public IError CreateMailServiceError()
        {
            return new Error() { code = "100081", message = "Email Service Error" };
        }
        public IError CreateTwilioServiceError()
        {
            return new Error() { code = "100082", message = "Twilio Service Error" };
        }
        public IError CreateServiceUnavailable()
        {
            return new Error() { code = "100083", message = "Service Unavailable" };
        }
        public IError CreateFailedToDeserializeJson()
        {
            return new Error() { code = "100084", message = "Failed To Deserialize JSON" };
        }
        public IError CreateDbExecutionFailed()
        {
            return new Error() { code = "100085", message = "Database execution failed during operation." };
        }
        public IError CreateDbException()
        {
            return new Error() { code = "100086", message = "Exception occured during database operation." };
        }
        public IError CreateFailedInitDatabase()
        {
            return new Error() { code = "100087", message = "Failed init database" };
        }
        public IError CreateInternalServerError()
        {
            return new Error() { code = "100088", message = "Internal server error" };
        }
        public IError CreateErrorUncatched()
        {
            return new Error() { code = "100089", message = "Error Uncatched"};
        }

        // 10009X- MongoDB Related
        public IError CreateDbInternalError()
        {
            return new Error() { code = "100090", message = "Internal MongoDB error" };
        }
        public IError CreateDbBadValue()
        {
            return new Error() { code = "100091", message = "MongoDB bad value input" };
        }
        public IError CreateDbDuplicateKey()
        {
            return new Error() { code = "100092", message = "MongoDB duplicate key existed" };
        }
        public IError CreateDbDataMalfunction()
        {
            return new Error() { code = "100093", message = "MongoDB data malfunction" };
        }
        public IError CreateDbNoSuchKey()
        {
            return new Error() { code = "100094", message = "No such key exists in MongoDB" };
        }
        public IError CreateDbHostUnreachable()
        {
            return new Error() { code = "100095", message = "MongoDB host unreachable" };
        }
        public IError CreateDbHostNotFound()
        {
            return new Error() { code = "100096", message = "MongoDB host not found" };
        }
        public IError CreateDbFailedToGetData()
        {
            return new Error() { code = "100097", message = "MongoDB failed to get data access" };
        }
        public IError CreateDbFailedCountData()
        {
            return new Error() { code = "100098", message = "MongoDB failed to count data" };
        }

        /// 1001XX - Core Framework


        /// 1002XX - Core Log


        /// 1003XX - Core MySQL

        // 10031X - Base MySQL Repository
        public IError CreateMySQLNotSupportedMethodError()
        {
            return new Error() { code = "100311", message = "Failed to execute this method because not supported." };
        }

        // 10032X - MySQL Health Check
        public IError CreateMySQLInternalError()
        {
            return new Error() { code = "100321", message = "Internal MySQL error" };
        }


        /// 1004XX - Core MongoDB

        // 10041X - Base Mongo Repository
        public IError CreateMongoNotSupportedMethodError()
        {
            return new Error() { code = "100411", message = "Failed to execute this method because not supported." };
        }

        // 10042X - Mongo Health Check
        public IError CreateMongoHealthCreateException()
        {
            return new Error() { code = "100421", message = "Health check CREATE cannot be executed because MongoDB exception has occurred." };
        }

        public IError CreateMongoHealthGetNull()
        {
            return new Error() { code = "100422", message = "Health check GET failed because result is null." };
        }

        public IError CreateMongoHealthGetException()
        {
            return new Error() { code = "100423", message = "Health check GET failed because MongoDB exception has occurred." };
        }

        public IError CreateMongoHealthDeleteFalse()
        {
            return new Error() { code = "100424", message = "Health check DELETE failed because result is returning false." };
        }

        public IError CreateMongoHealthDeleteException()
        {
            return new Error() { code = "100425", message = "Health check DELETE failed because MongoDB exception has occurred." };
        }

        // 10043X - Mongo Health Check With Timer 
        public IError CreateMongoHealthTimerCreateException()
        {
            return new Error() { code = "100431", message = "Health check CREATE with timer cannot be executed because an exception has occurred." };
        }

        public IError CreateMongoHealthTimerGetNull()
        {
            return new Error() { code = "100432", message = "Health check GET with timer failed because result is null." };
        }

        public IError CreateMongoHealthTimerGetException()
        {
            return new Error() { code = "100433", message = "Health check GET with timer failed because an exception has occurred." };
        }

        public IError CreateMongoHealthTimerDeleteFalse()
        {
            return new Error() { code = "100434", message = "Health check DELETE with timer failed because result is returning false." };
        }

        public IError CreateMongoHealthTimerDeleteException()
        {
            return new Error() { code = "100435", message = "Health check DELETE with timer failed because an exception has occurred." };
        }

        /// 1005XX - Core Health

        // 10051X - Service Account Health
        public IError CreateFailedConnectServiceAccount()
        {
            return new Error() { code = "100511", message = "Failed To Connect With Service Account." };
        }

        public IError CreateServiceAccountAccessException()
        {
            return new Error() { code = "100512", message = "Exception occurred when trying to access Service Account." };
        }

        // 10052X - RabbitMQ Health 
        public IError CreateRabbitMQConnectException()
        {
            return new Error() { code = "100521", message = "Health check CONNECT to RabbitMQ has failed because an exception has occurred." };
        }

        /// 1006XX - Core MessageBroker
        public IError CreateFailedListenRabbitMQ()
        {
            return new Error() { code = "100601", message = "Failed to listen to RabbitMQ server." };
        }

        public IError CreateFailedPublishRabbitMQ()
        {
            return new Error() { code = "100602", message = "Failed to publish to RabbitMQ server." };
        }
    }
}
