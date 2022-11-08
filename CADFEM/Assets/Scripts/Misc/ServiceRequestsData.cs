using System;
using System.Collections.Generic;
using UnityEngine;

namespace RequestParamClasses {
    public class ThingParam {
        public string thing;
    }

    public class WorkLogIdParam {
        public int work_log_id;
    }

    public class WorkLogOperationIdParam {
        public int work_log_operation_id;
    }

    public class WorkInstructionOperationCpIdParam {
        public int work_instruction_operation_cp_id;
    }

    public class ControlParamSaveValueInt {
        public int work_log_operation_cp_id;
        public int value_fact;
    }

    public class UserEnteredParams {
        public Sprite Sprite;
        public ControlParamForSave[] ControlParams;
    }

    public class ControlParamForSave {
        public int work_log_operation_cp_id;
        public float? value_fact;
        public bool? state_fact;
        public int selected_id;
        public string selected_text;
    }

    public class WorkInstructionOperationIdParam {
        public int work_instruction_operation_id;
    }
    
    public class WorkInstructionIdParam {
        public int work_instruction_id;
    }
    
    public class WorkLogOperationCpIdParam {
        public int work_log_operation_cp_id;
    }

    public class RequestId {
        public int request_id;
    }

    public class WorkLogIdWorkInstructionIdParam {
        public int work_log_id;
        public int work_instruction_id;
    }
    
    public class WorkLogOperationSaveFotoParam {
        public int work_log_operation_id;
        public string content;
    }
    public class WorkLogSaveCommentParam {
        public int work_log_id;
        public string comment;
    }

    public class UserInfo {
        public string full_name;
        public string username;
        public int person_id;
    }

}