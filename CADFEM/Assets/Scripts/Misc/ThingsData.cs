using RequestParamClasses;
using UnityEngine;

namespace ClassesForJsonDeserialize {

    [System.Serializable]
    public class Status {
        public string status;
    }

    [System.Serializable]
    public class WorkLogOperationDropDownRow {
        public int id;
        public int work_log_operation_cp_id;
        public string name;
        public bool is_selected;
    }

    [System.Serializable]
    public class WorkLogOperationDropDownRows {
        public WorkLogOperationDropDownRow[] rows;
    }


    [System.Serializable]
    public class ControlParamDropDownRow {
        public int id;
        public int work_instruction_operation_cp_id;
        public string name;

    }

    [System.Serializable]
    public class ControlParamDropDownRows {
        public ControlParamDropDownRow[] rows;
    }

    [System.Serializable]
    public class ControlParam {
        public int id;
        public string operation_cp_type_code;
        public string name;
        public float value_nominal;
        public float value_fact;
        public string value_unit;
        public bool state_nominal;
        public bool state_fact;
        public string drop_down_selected;
        public bool is_complete;
        public int selected_id;

        public WorkLogOperationDropDownRow[] DropDownRows;
    }

    [System.Serializable]
    public class ControlParams {
        public ControlParam[] rows;
    }

    [System.Serializable]
    public class Operation {
        public int id;
        public string code;
        public string mark;
        public string mark_with_zeroes;
        public string work_log_id;
        public string asset_part_id;
        public string name;
        public string description;
        public string operation_status_code;
        public string operation_status_color;
        public string executor_person_id;
        public string duration_minutes;
        public string url_image;
        public string url_video;
        public string url_document;
        public string url_cad;
        public string created_dt;
        public string creator_person_id;
        public string modified_dt;
        public string modifier_person_id;
        public bool is_foto_required;
        public string url_foto;
        public int work_instruction_operation_id;
        public string work_instruction_id;
        public string fact_start_dt;
        public string fat_end_dt;
        public int control_params_count;
        
        // данные не из запроса 
        public Sprite Sprite;
        public ControlParam[] ControlParams;
        public string IN_WORK_COLOR;
        public string COMPLETE_COLOR;
    }

    [System.Serializable]
    public class Operations {
        public Operation[] rows;
    }

    [System.Serializable]
    public class TaskData {
        public string row_id;
        public string row_code;
        public string row_type_code;
        public string row_type_name;
        public string row_status_code;
        public string row_status_name;
        public string row_status_color;
        public string row_comment;
        public int request_id;
        public int work_log_id;
    }

    [System.Serializable]
    public class TasksList {
        public TaskData[] rows;
    }

    [System.Serializable]
    public class TagValue {
        public string tag_full;
        public string tag;
        public float value;
        public string unit;
        public string fill;
        public string comment;
        public string text;
    }

    [System.Serializable]
    public class SensorsData {
        public TagValue[] rows;
    }

    [System.Serializable]
    public class CreatedWorkLog {
        public int id;
        public string code;
        public int asset_id;
        public int assigned_person_id;
        public string safety_rules;
        public string description;
    }

    [System.Serializable]
    public class CreatedWorkLogs {
        public CreatedWorkLog[] rows;
    }

    [System.Serializable]
    public class SelectedCPFromLastOperation {
        public int id;
        public int work_log_operation_cp_id;
        public string name;
        public int work_instruction_id;
        private bool is_selected;
    }

    [System.Serializable]
    public class SelectedCPFromLastOperationArray {
        public SelectedCPFromLastOperation[] rows;
    }

    [System.Serializable]
    public class WorkLogInfo {
        public int id;
        public string code;
        public int asset_id;
        public int assigned_person_id;
        public string fact_start_dt;
        public string fact_end_dt;
        public string comment;
        public string work_log_status_code;
        public int request_id;
        public int duration_seconds;
        public int main_work_instruction_id;
        public string name;
        public string description;
        public string safety_rules;
        public bool is_fault_request;
        public bool is_new_operations_append;
    }
[System.Serializable]
    public class WorkInstructionInfo {
        public int id;
        public string code;
        public string work_instruction_type_code;
        public string name;
        public string description;
        public string safety_rules;
        public bool is_removed;
        public int creator_person_id;
        public int modifier_person_id;
    }
    [System.Serializable]
    public class WorkInstructionInfoRows {
        public WorkInstructionInfo[] rows;
    }

    [System.Serializable]
    public class GetInfoArray {
        public WorkLogInfo[] rows;
    }

    [System.Serializable]
    public class SavePhotoStatus {
        public bool is_check_ok;
        public string[] messages;
    }

    [System.Serializable]
    public class TagStateCircles {
        public string inverterCircle_state;
        public int inverterCircle_value;
        public string rectifierCircle_state;
        public int rectifierCircle_value;
        public string switcherCircle_state;
        public int switcherCircle_value;
    }

    [System.Serializable]
    public class TagBaseLabels {
        public string asset_name_tag;
        public string battID_tag;
    }

    [System.Serializable]
    public class TagStatus {
        public bool IsConnected;
        public string thing;

        public string OutputMode;
        public string OutputMode_FillColor;
        public string BatteryState;
        public string BatteryState_FillColor;
    }

    [System.Serializable]
    public class TagValues {
        public TagValue Q1;
        public TagValue Q2;
        public TagValue Q4;
        public TagValue UL1;
        public TagValue UL2;
        public TagValue UL3;
        public TagValue outIL1;
        public TagValue outIL2;
        public TagValue outIL3;
        public TagValue loadL1;
        public TagValue loadL2;
        public TagValue loadL3;
        public TagValue outUL1;
        public TagValue outUL2;
        public TagValue outUL3;
        public TagValue bypassUL1;
        public TagValue bypassUL2;
        public TagValue bypassUL3;
        public TagValue batteryUL1;
        public TagValue batteryTime;
        public TagValue batteryTemp;

        public TagStatus status;
        public TagBaseLabels base_labels;
        public TagStateCircles state_circles;
    }
    
    [System.Serializable]
    public class OperationStatusesArray {
        public OperationStatus[] rows;
    }

    [System.Serializable]
    public class OperationStatus {
        public string code;
        public string name;
        public string color;
    }
}