using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace K2IManageObjects
{
    public enum EntryType
    {
        Workspace,
        Folder,
        Document
    }

    public abstract class IMObject
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "wstype")]
        public string WsType { get; set; }

        private PropertyInfo[] _PropertyInfos = null;

        public IMObject()
        {
            WsType = "UNKNOWN";
        }

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

        public bool IsEmail
        {
            get { return string.Equals(WsType, "folder", System.StringComparison.OrdinalIgnoreCase); }
        }

        public bool IsDocument
        {
            get { return string.Equals(WsType, "document", System.StringComparison.OrdinalIgnoreCase); }
        }

        public bool IsDocumentShortcut
        {
            get { return string.Equals(WsType, "document_shortcut", System.StringComparison.OrdinalIgnoreCase); }
        }

        public bool IsFolder
        {
            get { return string.Equals(WsType, "folder", System.StringComparison.OrdinalIgnoreCase); }
        }

        public bool IsFolderShortcut
        {
            get { return string.Equals(WsType, "folder_shortcut", System.StringComparison.OrdinalIgnoreCase); }
        }

        public bool IsWorkspace
        {
            get { return string.Equals(WsType, "folder", System.StringComparison.OrdinalIgnoreCase); }
        }

        public bool IsWorkspaceShortcut
        {
            get { return string.Equals(WsType, "folder_shortcut", System.StringComparison.OrdinalIgnoreCase); }
        }
    }

    public abstract class IMDBObject : IMObject
    {
        [JsonProperty(PropertyName = "database")]
        public string Database { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "default_security")]
        public string DefaultSecurity { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        public IMDBObject()
        {
            Name = "UNKNOWN";
        }
    }

    public abstract class IMCutomFieldsObject : IMDBObject
    {
        [JsonProperty(PropertyName = "custom1")]
        public string Custom1 { get; set; }

        [JsonProperty(PropertyName = "custom2")]
        public string Custom2 { get; set; }

        [JsonProperty(PropertyName = "custom3")]
        public string Custom3 { get; set; }

        [JsonProperty(PropertyName = "custom4")]
        public string Custom4 { get; set; }

        [JsonProperty(PropertyName = "custom5")]
        public string Custom5 { get; set; }

        [JsonProperty(PropertyName = "custom6")]
        public string Custom6 { get; set; }

        [JsonProperty(PropertyName = "custom7")]
        public string Custom7 { get; set; }

        [JsonProperty(PropertyName = "custom8")]
        public string Custom8 { get; set; }

        [JsonProperty(PropertyName = "custom9")]
        public string Custom9 { get; set; }

        [JsonProperty(PropertyName = "custom10")]
        public string Custom10 { get; set; }

        [JsonProperty(PropertyName = "custom11")]
        public string Custom11 { get; set; }

        [JsonProperty(PropertyName = "custom12")]
        public string Custom12 { get; set; }

        [JsonProperty(PropertyName = "custom13")]
        public string Custom13 { get; set; }

        [JsonProperty(PropertyName = "custom14")]
        public string Custom14 { get; set; }

        [JsonProperty(PropertyName = "custom15")]
        public string Custom15 { get; set; }

        [JsonProperty(PropertyName = "custom16")]
        public string Custom16 { get; set; }

        [JsonProperty(PropertyName = "custom17")]
        public int Custom17 { get; set; }

        [JsonProperty(PropertyName = "custom18")]
        public int Custom18 { get; set; }

        [JsonProperty(PropertyName = "custom19")]
        public int Custom19 { get; set; }

        [JsonProperty(PropertyName = "custom20")]
        public int Custom20 { get; set; }

        [JsonProperty(PropertyName = "custom21")]
        public string Custom21 { get; set; }

        [JsonProperty(PropertyName = "custom22")]
        public string Custom22 { get; set; }

        [JsonProperty(PropertyName = "custom23")]
        public string Custom23 { get; set; }

        [JsonProperty(PropertyName = "custom24")]
        public string Custom24 { get; set; }

        [JsonProperty(PropertyName = "custom25")]
        public bool Custom25 { get; set; }

        [JsonProperty(PropertyName = "custom26")]
        public bool Custom26 { get; set; }

        [JsonProperty(PropertyName = "custom27")]
        public bool Custom27 { get; set; }

        [JsonProperty(PropertyName = "custom28")]
        public bool Custom28 { get; set; }

        [JsonProperty(PropertyName = "custom29")]
        public string Custom29 { get; set; }

        [JsonProperty(PropertyName = "custom30")]
        public string Custom30 { get; set; }

        [JsonProperty(PropertyName = "custom1_description")]
        public string Custom1Description { get; set; }

        [JsonProperty(PropertyName = "custom2_description")]
        public string Custom2Description { get; set; }

        [JsonProperty(PropertyName = "custom3_description")]
        public string Custom3Description { get; set; }

        [JsonProperty(PropertyName = "custom4_description")]
        public string Custom4Description { get; set; }

        [JsonProperty(PropertyName = "custom5_description")]
        public string Custom5Description { get; set; }

        [JsonProperty(PropertyName = "custom6_description")]
        public string Custom6Description { get; set; }

        [JsonProperty(PropertyName = "custom7_description")]
        public string Custom7Description { get; set; }

        [JsonProperty(PropertyName = "custom8_description")]
        public string Custom8Description { get; set; }

        [JsonProperty(PropertyName = "custom9_description")]
        public string Custom9Description { get; set; }

        [JsonProperty(PropertyName = "custom10_description")]
        public string Custom10Description { get; set; }

        [JsonProperty(PropertyName = "custom11_description")]
        public string Custom11Description { get; set; }

        [JsonProperty(PropertyName = "custom12_description")]
        public string Custom12Description { get; set; }

        [JsonProperty(PropertyName = "custom29_description")]
        public string Custom29Description { get; set; }

        [JsonProperty(PropertyName = "custom30_description")]
        public string Custom30Description { get; set; }
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

        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount { get; set; }

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
        public bool indexable { get; set; }

        public bool shadow { get; set; }

        public int retain { get; set; }

        public int field_required { get; set; }

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
        public bool enabled { get; set; }

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

    public class IMDocument : IMCutomFieldsObject
    {
        [JsonProperty(PropertyName = "document_number")]
        public int DocumentNumber { get; set; }

        public int version { get; set; }

        public string alias { get; set; }

        public string author { get; set; }

        [JsonProperty(PropertyName = "operator")]
        public string Operator { get; set; }

        public string type { get; set; }

        [JsonProperty(PropertyName = "class")]
        public string Clazz { get; set; }

        public string sub_class { get; set; }

        public string edit_date { get; set; }

        public string create_date { get; set; }

        public int retain_days { get; set; }

        public int size { get; set; }

        public bool indexable { get; set; }

        public string is_related { get; set; }

        public string location { get; set; }

        [JsonProperty(PropertyName = "last_user")]
        public string LastUser { get; set; }

        [JsonProperty(PropertyName = "in_use")]
        public bool InUse { get; set; }

        [JsonProperty(PropertyName = "in_use_by")]
        public string InUseBy { get; set; }

        public bool is_checked_out { get; set; }

        [JsonProperty(PropertyName = "archived")]
        public bool Archived { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "access")]
        public string Access { get; set; }

        public string checkout_path { get; set; }

        public string checkout_due_date { get; set; }

        public string checkout_comment { get; set; }

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

        public string iwl { get; set; }

        public string workspace_id { get; set; }

        public IMWarning[] warnings { get; set; }

        public string Recent()
        {
            return "/documents/recent";
        }

        public string Download()
        {
            return "/documents/" + Id + "/download";
        }

        public string Versions()
        {
            return "/documents/" + Id + "/download";
        }
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

        public List<IMDocument> Docunments(K2IMInterface.IMSession session)
        {
            string url = "folders/" + Id + "/documents";

            return JsonConvert.DeserializeObject<IMItemList<IMDocument>>(session.MakeGetCall(url)).data;
        }

        public List<IMFolder> Children(K2IMInterface.IMSession session)
        {
            string url = "folders/" + Id + "/children";

            return JsonConvert.DeserializeObject<IMItemList<IMFolder>>(session.MakeGetCall(url)).data;
        }

        public List<IMDocument> SearchFolders(K2IMInterface.IMSession session)
        {
            string url = "folders/" + Id + "/folders/search";

            return JsonConvert.DeserializeObject<IMItemList<IMDocument>>(session.MakeGetCall(url)).data;
        }

        public List<IMFolder> SubFolders(K2IMInterface.IMSession session)
        {
            string url = "folders/" + Id + "/subfolders";

            return JsonConvert.DeserializeObject<IMItemList<IMFolder>>(session.MakeGetCall(url)).data;
        }
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

    public class IMWorkspace : IMCutomFieldsObject
    {
        public string activity_date { get; set; }

        public string comment { get; set; }

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

        public int document_number { get; set; }

        public string author { get; set; }

        public string author_description { get; set; }

        [JsonProperty(PropertyName = "class")]
        public string Clazz { get; set; }

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
        public string Operator { get; set; }

        public string operator_description { get; set; }

        public int size { get; set; }

        public string type { get; set; }

        public int version { get; set; }

        public string conversation_id { get; set; }

        public string conversation_name { get; set; }

        public string subject { get; set; }

        public bool indexable { get; set; }

        public bool is_external { get; set; }

        public string location { get; set; }

        public int retain_days { get; set; }

        public List<IMFolder> Folders(K2IMInterface.IMSession session)
        {
            string url = "workspaces/" + Id + "/children";

            return JsonConvert.DeserializeObject<IMItemList<IMFolder>>(session.MakeGetCall(url)).data;
        }

        public List<IMFolder> Recent(K2IMInterface.IMSession session)
        {
            string url = "workspaces/recent";

            return JsonConvert.DeserializeObject<IMItemList<IMFolder>>(session.MakeGetCall(url)).data;
        }
    }

    public class IMTemplate : IMDBObject
    {
        public string activity_date { get; set; }

        public string comment { get; set; }

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

        public string owner { get; set; }

        public string owner_description { get; set; }

        public string sub_class { get; set; }

        public string subtype { get; set; }

        public string view_type { get; set; }

        public string workspace_id { get; set; }

        public string workspace_name { get; set; }

        public int document_number { get; set; }

        public string author { get; set; }

        public string author_description { get; set; }

        [JsonProperty(PropertyName = "class")]
        public string Clazz { get; set; }

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
        public string Operator { get; set; }

        public int size { get; set; }

        public string type { get; set; }

        public int version { get; set; }

        public string conversation_id { get; set; }

        public string conversation_name { get; set; }

        public string subject { get; set; }

        public bool indexable { get; set; }

        public bool is_external { get; set; }

        public string location { get; set; }

        public int retain_days { get; set; }
    }
}
