using RequestParamClasses;
using ClassesForJsonDeserialize;

public class ThingWorksParamCreator {

    public WorkLogIdParam WorkLogIdCreate(int workLogId){
        return new WorkLogIdParam { work_log_id = workLogId };
    }

    public RequestId RequestIdCreate(int requestId){
        return new RequestId() { request_id = requestId };
    }

    public WorkLogOperationIdParam WorkLogOperationIdCreate(int workLogOperationId){
        return new WorkLogOperationIdParam { work_log_operation_id = workLogOperationId };
    }

    public WorkLogOperationCpIdParam WorkLogOperationCpIdCreate(int workLogOperationCpId){
        return new WorkLogOperationCpIdParam { work_log_operation_cp_id = workLogOperationCpId };
    }

    public WorkLogIdWorkInstructionIdParam WorkLogIdWorkInstructionIdCreate(int workLogId, int workInstructionId){
        return new WorkLogIdWorkInstructionIdParam { work_log_id = workLogId, work_instruction_id = workInstructionId };
    }
    
    public WorkLogOperationSaveFotoParam WorkLogOperationSaveFotoCreate(int workLogOperationId, string content){
        return new WorkLogOperationSaveFotoParam {  work_log_operation_id = workLogOperationId, content = content};
    }

    public WorkInstructionOperationIdParam WorkInstructionOperationIdCreate(int workInstructionOperationId){
        return new WorkInstructionOperationIdParam { work_instruction_operation_id = workInstructionOperationId };
    }

    public WorkInstructionIdParam WorkInstructionIdCreate(int workInstructionId){
        return new WorkInstructionIdParam() { work_instruction_id = workInstructionId };
    }

    public WorkInstructionOperationCpIdParam WorkInstructionOperationCpIdCreate(int workInstructionOperationCpId){
        return new WorkInstructionOperationCpIdParam { work_instruction_operation_cp_id = workInstructionOperationCpId };
    }

    public ThingParam ThingNameCreate(string assetName){
        return new ThingParam() { thing = assetName };
    }
    
    public WorkLogSaveCommentParam WorkLogSaveCommentCreate(int workLogId, string comment){
        return new WorkLogSaveCommentParam { work_log_id = workLogId, comment = comment };
    }
}
