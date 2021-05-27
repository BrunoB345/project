using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSpreadsheetImporterBackEnd.Services.JSONResponses
{
    public class JSONResponseFields
    {
        public string Result { get; set; }
        public Field[] fieldList { get; set; }

        public class Field
        {
            public string schema_fld_id { get; set; }
            public string schema_id { get; set; }
            public string txt_prompt { get; set; }
            public string txt_name { get; set; }
            public string txt_description { get; set; }
            public string fk_field_type { get; set; }
            public string fk_field_type_name { get; set; }
            public string n_db_size { get; set; }
            public string fk_content_schema { get; set; }
            public string fk_content_schema_name { get; set; }
            public string fk_content_channel { get; set; }
            public string fk_content_channel_name { get; set; }
            public string fk_content_criteria { get; set; }
            public string fk_content_template { get; set; }
            public string fk_content_template_name { get; set; }
            public string txt_criteria_params { get; set; }
            public string txt_content_field_list { get; set; }
            public string txt_default_value { get; set; }
            public string n_index { get; set; }
            public string b_optional { get; set; }
            public string n_backofficelayout { get; set; }
            public string txt_form_params { get; set; }
            public string n_order { get; set; }
            public string fk_group { get; set; }
            public string b_unique { get; set; }
            public string txt_validation_regex { get; set; }
            public string n_columns { get; set; }
            public string txt_format { get; set; }
            public string exist { get; set; }
            public string old_schema_field_id { get; set; }
        }
    }
}
