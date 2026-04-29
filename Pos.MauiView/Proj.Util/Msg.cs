using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.Util;

public class Msg
{
    public const string DataAccess = "Data Access";
    public const string BusinessLogic = "Business Logic";
    public const string ExecutionError = "Execution Error";
    public const string ValidationError = "Validation Error";
    public const string RetrieveError = "Retrieval Error";
    public const string NoItemFound = "No Item Found";
    public const string CantDelete = "Cannot delete";
    public const string IdMismatch = "ID mismatch";
    public const string Concurrency = "This record was already updated by another user";
    public const string InvalidId = "Invalid ID";
    public const string InvalidRequestBody = "Invalid request body";
    public const string InvalidData = "Invalid data";
    
    public static string CreatedSuccessfully(string item)
    {
        return $"{item} created successfully.";
    }

    public static string UpdatedSuccessfully(string item)
    {
        return $"{item} updated successfully.";
    }

    public static string DeletedSuccessfully(string item)
    {
        return $"{item} deleted successfully.";
    }

}
