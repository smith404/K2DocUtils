using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace K2IManageObjects
{
    public abstract class IMObject
    {
        public string id { get; set; }

        private PropertyInfo[] _PropertyInfos = null;

        public override string ToString()
        {
            if (_PropertyInfos == null)
                _PropertyInfos = GetType().GetProperties();

            var sb = new StringBuilder();

            foreach (var info in _PropertyInfos)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }

            return sb.ToString();
        }
    }

    public abstract class IMDBObject : IMObject
    {
        public string database { get; set; }
    }

    public class IMItem<IMObject>
    {
        public IMObject data { get; set; }

        public override string ToString()
        {
            return data.ToString();
        }
    }

    public class IMItemList<IMObject>
    {
        public List<IMObject> data { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            data.ForEach(delegate (IMObject obj)
            {
                sb.AppendLine(obj.ToString());
            });

            return sb.ToString();
        }
    }

    public class IMAppSetupItem : IMDBObject
    {
        public string name { get; set; }

        public bool primary { get; set; }

        public string location { get; set; }

        public bool dde { get; set; }

        public string dde_app_name { get; set; }

        public string dde_topic { get; set; }

        public string dde_open { get; set; }

        public string dde_read_open { get; set; }

        public string dde_print { get; set; }

        public string dde_print_1 { get; set; }

        public string integration_mode { get; set; }
    }

    public class IMCaption : IMDBObject
    {
        public string label { get; set; }

        public int locale { get; set; }

        public int num { get; set; }

        public int ss_num { get; set; }

        public string type { get; set; }
    }

    public class IMClass : IMDBObject
    {
        public string description { get; set; }

        public bool indexable { get; set; }

        public bool shadow { get; set; }

        public int retain { get; set; }

        public int field_required { get; set; }

        public string default_security { get; set; }

        public bool hipaa { get; set; }

        public string[] field_required_details { get; set; }

        public bool subclass_required { get; set; }

        public bool sub_class_required { get; set; }
    }

    public class IMDatabase : IMObject
    {
        public string type { get; set; }
    }

    public class IMCustomField : IMObject
    {
        public string description { get; set; }

        public bool enabled { get; set; }

        public string wstype { get; set; }

        public string activity_date { get; set; }
    }

    public class IMWarning
    {
        public string field { get; set; }

        public string error { get; set; }

    }

    public class IMItem : IMObject
    {
        public string attachment_id { get; set; }

        public string name { get; set; }

        public int size { get; set; }
    }

    public class IMDocument : IMDBObject
    {
        [JsonProperty(PropertyName = "document_number")]
        public int DocumentNumber { get; set; }

        public int version { get; set; }

        public string name { get; set; }

        public string alias { get; set; }

        public string author { get; set; }

        [JsonProperty(PropertyName = "operator")]
        public string opcode { get; set; }

        public string type { get; set; }

        [JsonProperty(PropertyName = "class")]
        public string clazz { get; set; }

        public string sub_class { get; set; }

        public string edit_date { get; set; }

        public string create_date { get; set; }

        public int retain_days { get; set; }

        public int size { get; set; }

        public bool indexable { get; set; }

        public string is_related { get; set; }

        public string location { get; set; }

        public string default_security { get; set; }

        [JsonProperty(PropertyName = "last_user")]
        public string LastUser { get; set; }

        public string in_use_by { get; set; }

        public bool in_use { get; set; }

        public bool is_checked_out { get; set; }

        public bool archived { get; set; }

        public string comment { get; set; }

        public string custom1 { get; set; }

        public string custom2 { get; set; }

        public string custom3 { get; set; }

        public string custom4 { get; set; }

        public string custom5 { get; set; }

        public string custom6 { get; set; }

        public string custom7 { get; set; }

        public string custom8 { get; set; }

        public string custom9 { get; set; }

        public string custom10 { get; set; }

        public string custom11 { get; set; }

        public string custom12 { get; set; }

        public string custom13 { get; set; }

        public string custom14 { get; set; }

        public string custom15 { get; set; }

        public string custom16 { get; set; }

        public int custom17 { get; set; }

        public int custom18 { get; set; }

        public int custom19 { get; set; }

        public int custom20 { get; set; }

        public string custom21 { get; set; }

        public string custom22 { get; set; }

        public string custom23 { get; set; }

        public string custom24 { get; set; }

        public bool custom25 { get; set; }

        public bool custom26 { get; set; }

        public bool custom27 { get; set; }

        public bool custom28 { get; set; }

        public string custom29 { get; set; }

        public string custom30 { get; set; }

        public string access { get; set; }

        public string checkout_path { get; set; }

        public string checkout_due_date { get; set; }

        public string checkout_comment { get; set; }

        public string custom1_description { get; set; }

        public string custom2_description { get; set; }

        public string custom3_description { get; set; }

        public string custom4_description { get; set; }

        public string custom5_description { get; set; }

        public string custom6_description { get; set; }

        public string custom7_description { get; set; }

        public string custom8_description { get; set; }

        public string custom9_description { get; set; }

        public string custom10_description { get; set; }

        public string custom11_description { get; set; }

        public string custom12_description { get; set; }

        public string custom29_description { get; set; }

        public string custom30_description { get; set; }

        public string author_description { get; set; }

        public string operator_description { get; set; }

        public string type_description { get; set; }

        public string class_description { get; set; }

        public string sub_class_description { get; set; }

        public string last_user_description { get; set; }

        public string in_use_by_description { get; set; }

        public string edit_time { get; set; }

        public string extension { get; set; }

        public string custom31 { get; set; }

        public string content_type { get; set; }

        public string edit_profile_date { get; set; }

        public string access_time { get; set; }

        public int folder_id { get; set; }

        public string checkout_date { get; set; }

        public string msg_id { get; set; }

        public bool is_external { get; set; }

        public bool has_attachment { get; set; }

        public bool is_external_as_normal { get; set; }

        public string effective_security { get; set; }

        public string declared_date { get; set; }

        public string file_create_date { get; set; }

        public string file_edit_date { get; set; }

        public string arch_req { get; set; }

        public bool is_hipaa { get; set; }

        public string is_enabled { get; set; }

        public string workspace_name { get; set; }

        public string linksite_url { get; set; }

        public string activity_date { get; set; }

        public bool is_latest_version { get; set; }

        public string custom31_description { get; set; }

        public bool is_restorable { get; set; }

        public bool is_most_recent_activity { get; set; }

        public int conversation_count { get; set; }

        public IMItem[] attachments { get; set; }

        public string nvp_value { get; set; }

        public string wstype { get; set; }

        public string iwl { get; set; }

        public string workspace_id { get; set; }

        public IMWarning[] warnings { get; set; }
    }

    public class IMEmailParticipant
    {
        public string address { get; set; }

        public string name { get; set; }
    }

    public class IMEmail : IMDocument
    {
        public string conversation_id { get; set; }

        public string conversation_name { get; set; }

        public string received_date { get; set; }

        public string sent_date { get; set; }

        public string subject { get; set; }

        public string bcc { get; set; }

        public string cc { get; set; }

        public string from { get; set; }

        public string to { get; set; }
    }

    public class IMFolder : IMDBObject
    {
        public string activity_date { get; set; }

        public string container_saved_search_id { get; set; }

        public string content_saved_search_id { get; set; }

        public string default_security { get; set; }

        public string description { get; set; }

        public string edit_date { get; set; }

        public string effective_security { get; set; }

        public string email { get; set; }

        public string folder_type { get; set; }

        public bool has_documents { get; set; }

        public bool has_security { get; set; }

        public bool has_subfolders { get; set; }

        public string imanage_share_eid { get; set; }

        public string imanage_share_url { get; set; }

        public string inherited_default_security { get; set; }

        public bool is_container_saved_search { get; set; }

        public bool is_content_saved_search { get; set; }

        public bool is_external { get; set; }

        public bool is_external_as_normal { get; set; }

        public string location { get; set; }

        public string my_matters_shortcut_id { get; set; }

        public string name { get; set; }

        public string owner { get; set; }

        public string owner_description { get; set; }

        public string parent_id { get; set; }

        public List<KeyValuePair<string, int>> profile { get; set; }

        public string project_custom1 { get; set; }

        public string project_custom2 { get; set; }

        public string project_custom3 { get; set; }

        public string security_policy { get; set; }

        public string subtype { get; set; }

        public string view_type { get; set; }

        public string workspace_id { get; set; }

        public string workspace_name { get; set; }

        public string wstype { get; set; }
    }

    public class IMGroup : IMDBObject
    {
        public string group_id { get; set; }

        public string groupid { get; set; }

        public int group_nos { get; set; }

        public string full_name { get; set; }

        public int group_number { get; set; }

        public string group_domain { get; set; }

        public bool is_external { get; set; }

        public bool enabled { get; set; }

        public string sync_id { get; set; }

        public string distinguished_name { get; set; }

        public string last_sync_ts { get; set; }
    }

    public class IMJournal
    {
        public int document_number { get; set; }

        public int version { get; set; }

        public string activity { get; set; }

        public string application_name { get; set; }

        public string activity_date { get; set; }

        public int duration { get; set; }

        public int pages_printed { get; set; }

        public string user { get; set; }

        public int activity_code { get; set; }

        public string location { get; set; }

        public string comments { get; set; }

        public string data1 { get; set; }

        public string data2 { get; set; }

        public string num1 { get; set; }

        public string num2 { get; set; }

        public string num3 { get; set; }

        public int sid { get; set; }

        public bool has_journal { get; set; }

        public string user_fullname { get; set; }

        public string database { get; set; }

        public string document_id { get; set; }
    }

    public class IMM1Bits
    {
        public bool import { get; set; }

        public bool checkout { get; set; }

        public bool release { get; set; }

        public bool delete { get; set; }

        public bool read_only { get; set; }

        public bool allow_index_search { get; set; }

        public bool display_public_documents { get; set; }

        public bool edit_previous_versions { get; set; }

        public bool edit_external_default_security { get; set; }

        public bool create_public_folder { get; set; }

        public bool create_public_search_folder { get; set; }

        public bool delete_public_folder { get; set; }

        public bool delete_public_search_folder { get; set; }

        public bool view_public_folder { get; set; }

        public bool view_public_search_folder { get; set; }

        public bool edit_external_default_security_folder { get; set; }
    }

    public class IMM2Bits
    {
        public bool use_import_tool { get; set; }

        public bool use_monitor_tool { get; set; }

        public bool use_admin_tool { get; set; }

        public bool document_view { get; set; }

        public bool external { get; set; }
    }

    public class IMM3Bits
    {
        public bool browse_workspace { get; set; }

        public bool search_workspace { get; set; }

        public bool author_workspace { get; set; }

        public bool share_workspace { get; set; }

        public bool customize_personal_views { get; set; }

        public bool customize_public_views { get; set; }

        public bool create_template { get; set; }

        public bool delete_workspace { get; set; }
    }

    public class IMM4Bits
    {
        public bool content_assistance { get; set; }

        public bool trustee_assistance { get; set; }

        public bool report { get; set; }

        public bool report_management { get; set; }

        public bool dms_job_operations { get; set; }

        public bool custom_metadata_management { get; set; }

        public bool system_metadata_management { get; set; }

        public bool role_management { get; set; }

        public bool trustee_management { get; set; }

        public bool govern_security_management { get; set; }

        public bool system_management { get; set; }

        public bool system_job_operations { get; set; }

        public bool admin_tier_3 { get; set; }

        public bool admin_tier_2 { get; set; }

        public bool admin_tier_1 { get; set; }

        public bool admin_custom { get; set; }

        public int tier { get; set; }
    }

    public class IMRole : IMDBObject
    {
        public string description { get; set; }

        public int m1 { get; set; }

        public int m2 { get; set; }

        public int m3 { get; set; }

        public IMM1Bits m1_bits { get; set; }

        public IMM2Bits m2_bits { get; set; }

        public IMM3Bits m3_bits { get; set; }

        public IMM4Bits m4_bits { get; set; }
    }

    public class IMSession : IMObject
    {
        public string application_name { get; set; }

        public string ip { get; set; }

        public string user_id { get; set; }

        public string persona { get; set; }

        public string last_active { get; set; }
    }

    public class IMUser : IMDBObject
    {
        public string user_id { get; set; }

        public string userid { get; set; }

        public string full_name { get; set; }

        public string location { get; set; }

        public string phone { get; set; }

        public string extension { get; set; }

        public bool login { get; set; }

        public string fax { get; set; }

        public string email { get; set; }

        public string doc_server { get; set; }

        public string preferred_database { get; set; }

        public string user_domain { get; set; }

        public int user_nos { get; set; }

        public string email2 { get; set; }

        public string email3 { get; set; }

        public string email4 { get; set; }

        public string email5 { get; set; }

        public string custom1 { get; set; }

        public string custom2 { get; set; }

        public string custom3 { get; set; }

        public string sync_id { get; set; }

        public string distinguished_name { get; set; }

        public string is_external { get; set; }

        public string secure_docserver { get; set; }

        public string exch_autodiscover { get; set; }

        public string edit_date { get; set; }
    }

    public class IMWorkspace : IMDBObject
    {
        public string activity_date { get; set; }

        public string comment { get; set; }

        public string default_security { get; set; }


        public string description { get; set; }

        public string effective_security { get; set; }

        public string edit_date { get; set; }

        public string email { get; set; }

        public string folder_type { get; set; }

        public bool has_documents { get; set; }

        public bool has_subfolders { get; set; }

        public bool is_container_saved_search { get; set; }

        public bool is_content_saved_search { get; set; }

        public bool is_external_as_normal { get; set; }

        public string last_user_description { get; set; }

        public string name { get; set; }

        public string owner { get; set; }

        public string owner_description { get; set; }

        public string project_custom1 { get; set; }

        public string project_custom2 { get; set; }

        public string project_custom3 { get; set; }

        public string sub_class { get; set; }

        public string subtype { get; set; }

        public string view_type { get; set; }

        public string workspace_id { get; set; }

        public string workspace_name { get; set; }

        public string wstype { get; set; }

        public int document_number { get; set; }

        public string author { get; set; }

        public string author_description { get; set; }

        [JsonProperty(PropertyName = "class")]
        public string clazz { get; set; }

        public string content_type { get; set; }

        public string create_date { get; set; }

        public string edit_profile_date { get; set; }

        public string extension { get; set; }

        public string file_create_date { get; set; }

        public string file_edit_date { get; set; }

        public bool has_attachment { get; set; }

        public bool in_use { get; set; }

        public bool is_checked_out { get; set; }

        public bool is_hipaa { get; set; }

        public bool is_restorable { get; set; }

        public string iwl { get; set; }

        public string last_user { get; set; }

        [JsonProperty(PropertyName = "operator")]
        public string opcode { get; set; }

        public string operator_description { get; set; }

        public int size { get; set; }

        public string type { get; set; }

        public int version { get; set; }

        public string conversation_id { get; set; }

        public string conversation_name { get; set; }

        public string subject { get; set; }

        public string custom1 { get; set; }

        public string custom1_description { get; set; }

        public string custom2 { get; set; }

        public string custom2_description { get; set; }

        public string custom3 { get; set; }

        public string custom3_description { get; set; }

        public string custom4 { get; set; }

        public string custom4_description { get; set; }

        public string custom5 { get; set; }

        public string custom5_description { get; set; }

        public string custom6 { get; set; }
        public string custom6_description { get; set; }

        public string custom7 { get; set; }

        public string custom7_description { get; set; }

        public string custom8 { get; set; }

        public string custom8_description { get; set; }

        public string custom9 { get; set; }

        public string custom9_description { get; set; }

        public string custom10 { get; set; }

        public string custom10_description { get; set; }

        public string custom11 { get; set; }

        public string custom11_description { get; set; }

        public string custom12 { get; set; }

        public string custom12_description { get; set; }

        public string custom13 { get; set; }

        public string custom14 { get; set; }

        public string custom15 { get; set; }

        public string custom16 { get; set; }

        public string custom17 { get; set; }

        public string custom18 { get; set; }

        public string custom19 { get; set; }

        public string custom20 { get; set; }

        public string custom21 { get; set; }

        public string custom22 { get; set; }

        public string custom23 { get; set; }

        public string custom24 { get; set; }

        public string custom25 { get; set; }

        public string custom26 { get; set; }

        public string custom27 { get; set; }

        public string custom28 { get; set; }

        public string custom29 { get; set; }

        public string custom29_description { get; set; }

        public string custom30 { get; set; }

        public string custom30_description { get; set; }

        public bool indexable { get; set; }

        public bool is_external { get; set; }

        public string location { get; set; }

        public int retain_days { get; set; }
    }
}
