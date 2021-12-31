﻿/*
 * Copyright (c) 2021. K2-Software
 * All software, both binary and source published by K2-Software (hereafter, Software)
 * is copyrighted by the author (hereafter, K2-Software) and ownership of all right, 
 * title and interest in and to the Software remains with K2-Software. By using or 
 * copying the Software, User agrees to abide by the terms of this Agreement.
 */

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace K2IManageObjects
{
    public enum EntryType
    {
        Workspace,
        Folder,
        Document,
        Email,
        Unknown
    }

    public class IMInstance
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string URL { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(System.Object that)
        {
            if ((that == null))
            {
                return false;
            }
            else
            {
                return Name.Equals(that.ToString());
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public abstract class IMObject
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "wstype")]
        public string WsType { get; set; }

        private PropertyInfo[] _PropertyInfos = null;

        public EntryType EntityType
        {
            get
            {
                if (string.Equals(WsType, "workspace", System.StringComparison.OrdinalIgnoreCase))
                {
                    return EntryType.Workspace;
                }
                else if (string.Equals(WsType, "workspace_shortcut", System.StringComparison.OrdinalIgnoreCase))
                {
                    return EntryType.Workspace;
                }
                else if (string.Equals(WsType, "folder", System.StringComparison.OrdinalIgnoreCase))
                {
                    return EntryType.Folder;
                }
                else if (string.Equals(WsType, "folder_shortcut", System.StringComparison.OrdinalIgnoreCase))
                {
                    return EntryType.Folder;
                }
                else if (string.Equals(WsType, "document", System.StringComparison.OrdinalIgnoreCase))
                {
                    return EntryType.Document;
                }
                else if (string.Equals(WsType, "document_shortcut", System.StringComparison.OrdinalIgnoreCase))
                {
                    return EntryType.Document;
                }
                else if (string.Equals(WsType, "email", System.StringComparison.OrdinalIgnoreCase))
                {
                    return EntryType.Email;
                }
                else
                {
                    return EntryType.Unknown;
                }
            }
        }
        public IMObject()
        {
            WsType = "UNKNOWN";
        }

        public override string ToString()
        {
            if (_PropertyInfos == null)
            {
                _PropertyInfos = GetType().GetProperties();
            }

            StringBuilder sb = new StringBuilder();

            foreach (PropertyInfo info in _PropertyInfos)
            {
                object value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }

            return sb.ToString();
        }

        public bool IsEmail
        {
            get { return string.Equals(WsType, "email", System.StringComparison.OrdinalIgnoreCase); }
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

    public class IMProfile
    {
        [JsonProperty(PropertyName = "doc_profile")]
        public IMDocProfile DocProfile { get; set; }
    }

    public class IMDocProfile
    {
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "class")]
        public string Clazz { get; set; }

        [JsonProperty(PropertyName = "extension")]
        public string Extension { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }

    public class IMJournal
    {
        [JsonProperty(PropertyName = "document_number")]
        public int DocumentNumber { get; set; }

        [JsonProperty(PropertyName = "version")]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "activity")]
        public string Activity { get; set; }

        [JsonProperty(PropertyName = "application_name")]
        public string ApplicationName { get; set; }

        [JsonProperty(PropertyName = "activity_date")]
        public string ActivityDate { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }

        [JsonProperty(PropertyName = "pages_printed")]
        public int PagesPrinted { get; set; }

        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "activity_code")]
        public int ActivityCode { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }

        [JsonProperty(PropertyName = "data1")]
        public string Data1 { get; set; }

        [JsonProperty(PropertyName = "data2")]
        public string Data2 { get; set; }

        [JsonProperty(PropertyName = "num1")]
        public string Num1 { get; set; }

        [JsonProperty(PropertyName = "num2")]
        public string Num2 { get; set; }

        [JsonProperty(PropertyName = "num3")]
        public string Num3 { get; set; }

        [JsonProperty(PropertyName = "sid")]
        public int SID { get; set; }

        [JsonProperty(PropertyName = "has_journal")]
        public bool HasJournal { get; set; }

        [JsonProperty(PropertyName = "user_fullname")]
        public string UserFullname { get; set; }

        [JsonProperty(PropertyName = "database")]
        public string Database { get; set; }

        [JsonProperty(PropertyName = "document_id")]
        public string DocumentId { get; set; }
    }

    public class IMM1Bits
    {
        [JsonProperty(PropertyName = "import")]
        public bool Import { get; set; }

        [JsonProperty(PropertyName = "checkout")]
        public bool Checkout { get; set; }

        [JsonProperty(PropertyName = "release")]
        public bool Release { get; set; }

        [JsonProperty(PropertyName = "delete")]
        public bool Delete { get; set; }

        [JsonProperty(PropertyName = "read_only")]
        public bool ReadOnly { get; set; }

        [JsonProperty(PropertyName = "allow_index_search")]
        public bool AllowIndexSearch { get; set; }

        [JsonProperty(PropertyName = "display_public_documents")]
        public bool DisplayPublicDocuments { get; set; }

        [JsonProperty(PropertyName = "edit_previous_versions")]
        public bool EditPreviousVersions { get; set; }

        [JsonProperty(PropertyName = "edit_external_default_security")]
        public bool EditExternalDefaultSecurity { get; set; }

        [JsonProperty(PropertyName = "create_public_folder")]
        public bool CreatePublicFolder { get; set; }

        [JsonProperty(PropertyName = "create_public_search_folder")]
        public bool CreatePublicSearchFolder { get; set; }

        [JsonProperty(PropertyName = "delete_public_folder")]
        public bool DeletePublicFolder { get; set; }

        [JsonProperty(PropertyName = "delete_public_search_folder")]
        public bool DeletePublicSearchFolder { get; set; }

        [JsonProperty(PropertyName = "view_public_folder")]
        public bool ViewPublicFolder { get; set; }

        [JsonProperty(PropertyName = "view_public_search_folder")]
        public bool ViewPublicSearchFolder { get; set; }

        [JsonProperty(PropertyName = "edit_external_default_security_folder")]
        public bool EditExternalDefaultSecurityFolder { get; set; }
    }

    public class IMM2Bits
    {
        [JsonProperty(PropertyName = "use_import_tool")]
        public bool UseImportTool { get; set; }

        [JsonProperty(PropertyName = "use_monitor_tool")]
        public bool UseMonitorTool { get; set; }

        [JsonProperty(PropertyName = "use_admin_tool")]
        public bool UseAdminTool { get; set; }

        [JsonProperty(PropertyName = "document_view")]
        public bool DocumentView { get; set; }

        [JsonProperty(PropertyName = "external")]
        public bool External { get; set; }
    }

    public class IMM3Bits
    {
        [JsonProperty(PropertyName = "browse_workspace")]
        public bool BrowseWorkspace { get; set; }

        [JsonProperty(PropertyName = "search_workspace")]
        public bool SearchWorkspace { get; set; }

        [JsonProperty(PropertyName = "author_workspace")]
        public bool AuthorWorkspace { get; set; }

        [JsonProperty(PropertyName = "share_workspace")]
        public bool ShareWorkspace { get; set; }

        [JsonProperty(PropertyName = "customize_personal_views")]
        public bool CustomizePersonalViews { get; set; }

        [JsonProperty(PropertyName = "customize_public_views")]
        public bool CustomizePublicViews { get; set; }

        [JsonProperty(PropertyName = "create_template")]
        public bool CreateTemplate { get; set; }

        [JsonProperty(PropertyName = "delete_workspace")]
        public bool DeleteWorkspace { get; set; }
    }

    public class IMM4Bits
    {
        [JsonProperty(PropertyName = "content_assistance")]
        public bool ContentAssistance { get; set; }

        [JsonProperty(PropertyName = "trustee_assistance")]
        public bool TrusteeAssistance { get; set; }

        [JsonProperty(PropertyName = "report")]
        public bool Report { get; set; }

        [JsonProperty(PropertyName = "report_management")]
        public bool ReportManagement { get; set; }

        [JsonProperty(PropertyName = "dms_job_operations")]
        public bool DmsJobOperations { get; set; }

        [JsonProperty(PropertyName = "custom_metadata_management")]
        public bool CustomMetadataManagement { get; set; }

        [JsonProperty(PropertyName = "system_metadata_management")]
        public bool SystemMetadataManagement { get; set; }

        [JsonProperty(PropertyName = "role_management")]
        public bool RoleManagement { get; set; }

        [JsonProperty(PropertyName = "trustee_management")]
        public bool TrusteeManagement { get; set; }

        [JsonProperty(PropertyName = "govern_security_management")]
        public bool GovernSecurityManagement { get; set; }

        [JsonProperty(PropertyName = "system_management")]
        public bool SystemManagement { get; set; }

        [JsonProperty(PropertyName = "system_job_operations")]
        public bool SystemJobOperations { get; set; }

        [JsonProperty(PropertyName = "admin_tier_3")]
        public bool AdminTier3 { get; set; }

        [JsonProperty(PropertyName = "admin_tier_2")]
        public bool AdminTier2 { get; set; }

        [JsonProperty(PropertyName = "admin_tier_1")]
        public bool AdminTier1 { get; set; }

        [JsonProperty(PropertyName = "admin_custom")]
        public bool AdminCustom { get; set; }

        [JsonProperty(PropertyName = "tier")]
        public int Tier { get; set; }
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

        public List<IMDBObject> Children(K2IMInterface.IMConnection session)
        {
            StringBuilder uri = new StringBuilder();

            switch (EntityType)
            {
                case EntryType.Workspace:
                    uri.Append("workspaces/");
                    break;
                case EntryType.Folder:
                    uri.Append("folders/");
                    break;
                default:
                    return new List<IMDBObject>();
            }

            uri.Append(Id);
            uri.Append("/children");

            return JsonConvert.DeserializeObject<IMItemList<IMDBObject>>(session.PerformGetCall(uri.ToString())).Data;
        }

        public string SearchString(string term, int offset, int limit, bool total)
        {
            StringBuilder uri = new StringBuilder();

            switch (EntityType)
            {
                case EntryType.Workspace:
                    uri.Append("workspaces/search");
                    break;
                case EntryType.Folder:
                    uri.Append("folders/search");
                    break;
                case EntryType.Document:
                    uri.Append("documents/search");
                    break;
                case EntryType.Email:
                    uri.Append("emails/search");
                    break;
            }

            uri.Append("?anywhere=");
            uri.Append(term);

            if (offset > 0)
            {
                uri.Append("&offset=");
                uri.Append(offset);
            }

            if (offset != 25)
            {
                uri.Append("&limit=");
                uri.Append(limit);
            }

            if (total)
            {
                uri.Append("&total=true");
            }

            return uri.ToString();
        }

        public string SearchString(List<string> terms)
        {
            StringBuilder uri = new StringBuilder();

            switch (EntityType)
            {
                case EntryType.Workspace:
                    uri.Append("workspaces/search");
                    break;
                case EntryType.Folder:
                    uri.Append("folders/search");
                    break;
                case EntryType.Document:
                    uri.Append("documents/search");
                    break;
                case EntryType.Email:
                    uri.Append("emails/search");
                    break;
            }

            bool firstTerm = true;
            foreach (string term in terms)
            {
                if (firstTerm)
                {
                    firstTerm = false;
                    uri.Append("?");
                }
                else
                {
                    uri.Append("&");
                }

                uri.Append(term);
            }

            return uri.ToString();
        }
    }

    public class IMSession : IMObject
    {
        [JsonProperty(PropertyName = "application_name")]
        public string ApplicationName { get; set; }

        [JsonProperty(PropertyName = "ip")]
        public string IP { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "persona")]
        public string Persona { get; set; }

        [JsonProperty(PropertyName = "last_active")]
        public string LastActive { get; set; }
    }

    public class IMGroup : IMDBObject
    {
        [JsonProperty(PropertyName = "group_id")]
        public string GroupId { get; set; }

        [JsonProperty(PropertyName = "groupid")]
        public string GID { get; set; }

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

    public class IMRole : IMDBObject
    {
        [JsonProperty(PropertyName = "m1")]
        public int M1 { get; set; }

        [JsonProperty(PropertyName = "m2")]
        public int M2 { get; set; }

        [JsonProperty(PropertyName = "m3")]
        public int M3 { get; set; }

        [JsonProperty(PropertyName = "m1_bits")]
        public IMM1Bits M1Bits { get; set; }

        [JsonProperty(PropertyName = "m2_bits")]
        public IMM2Bits M2Bits { get; set; }

        [JsonProperty(PropertyName = "m3_bits")]
        public IMM3Bits M3Bits { get; set; }

        [JsonProperty(PropertyName = "m4_bits")]
        public IMM4Bits M4Bits { get; set; }
    }

    public class IMUser : IMDBObject
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "userid")]
        public string UID { get; set; }

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "extension")]
        public string Extension { get; set; }

        [JsonProperty(PropertyName = "login")]
        public bool Login { get; set; }

        [JsonProperty(PropertyName = "fax")]
        public string Fax { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "doc_server")]
        public string DocServer { get; set; }

        [JsonProperty(PropertyName = "preferred_database")]
        public string PreferredDatabase { get; set; }

        [JsonProperty(PropertyName = "user_domain")]
        public string UserDomain { get; set; }

        [JsonProperty(PropertyName = "user_nos")]
        public int UserNos { get; set; }

        [JsonProperty(PropertyName = "email2")]
        public string Email2 { get; set; }

        [JsonProperty(PropertyName = "email3")]
        public string Email3 { get; set; }

        [JsonProperty(PropertyName = "email4")]
        public string Email4 { get; set; }

        [JsonProperty(PropertyName = "email5")]
        public string Email5 { get; set; }

        [JsonProperty(PropertyName = "custom1")]
        public string Custom1 { get; set; }

        [JsonProperty(PropertyName = "custom2")]
        public string Custom2 { get; set; }

        [JsonProperty(PropertyName = "custom3")]
        public string Custom3 { get; set; }

        [JsonProperty(PropertyName = "sync_id")]
        public string SyncId { get; set; }

        [JsonProperty(PropertyName = "distinguished_name")]
        public string DistinguishedName { get; set; }

        [JsonProperty(PropertyName = "is_external")]
        public string IsExternal { get; set; }

        [JsonProperty(PropertyName = "secure_docserver")]
        public string SecureDocserver { get; set; }

        [JsonProperty(PropertyName = "exch_autodiscover")]
        public string ExchAutodiscover { get; set; }

        [JsonProperty(PropertyName = "edit_date")]
        public string EditDate { get; set; }
    }

    public abstract class IMTarget : IMDBObject
    {
        [JsonProperty(PropertyName = "folder_type")]
        public string FolderType { get; set; }

        [JsonProperty(PropertyName = "doc_type")]
        public string DocType { get; set; }

        [JsonProperty(PropertyName = "document_number")]
        public int DocumentNumber { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "subtype")]
        public string Subtype { get; set; }
    }

    public class IMItem<IMObject>
    {
        [JsonProperty(PropertyName = "data")]
        public IMObject Data { get; set; }

        public override string ToString()
        {
            return Data.ToString();
        }
    }

    public class IMItemList<IMObject>
    {
        [JsonProperty(PropertyName = "data")]
        public List<IMObject> Data { get; set; }

        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount { get; set; }

        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            Data.ForEach(delegate (IMObject obj)
            {
                sb.AppendLine(obj.ToString());
            });

            return sb.ToString();
        }
    }

    public class IMAppSetupItem : IMDBObject
    {
        [JsonProperty(PropertyName = "primary")]
        public bool Primary { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "dde")]
        public bool DDE { get; set; }

        [JsonProperty(PropertyName = "dde_app_name")]
        public string DDEAppName { get; set; }

        [JsonProperty(PropertyName = "dde_topic")]
        public string DDETopic { get; set; }

        [JsonProperty(PropertyName = "dde_open")]
        public string DDEOpen { get; set; }

        [JsonProperty(PropertyName = "dde_read_open")]
        public string DDEReadOpen { get; set; }

        [JsonProperty(PropertyName = "dde_print")]
        public string DDEPrint { get; set; }

        [JsonProperty(PropertyName = "dde_print_1")]
        public string DDEPrint1 { get; set; }

        [JsonProperty(PropertyName = "integration_mode")]
        public string IntegrationMode { get; set; }
    }

    public class IMCaption : IMDBObject
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "locale")]
        public int Locale { get; set; }

        [JsonProperty(PropertyName = "num")]
        public int Num { get; set; }

        [JsonProperty(PropertyName = "ss_num")]
        public int SSNum { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }

    public class IMClass : IMDBObject
    {
        [JsonProperty(PropertyName = "indexable")]
        public bool Indexable { get; set; }

        [JsonProperty(PropertyName = "shadow")]
        public bool Shadow { get; set; }

        [JsonProperty(PropertyName = "retain")]
        public int Retain { get; set; }

        [JsonProperty(PropertyName = "field_required")]
        public int FieldRequired { get; set; }

        [JsonProperty(PropertyName = "hipaa")]
        public bool Hipaa { get; set; }

        [JsonProperty(PropertyName = "field_required_details")]
        public string[] FieldRequiredDetails { get; set; }

        [JsonProperty(PropertyName = "sub_class_required")]
        public bool SubclassRequired { get; set; }

        [JsonProperty(PropertyName = "sub_class_required")]
        public bool SubClassRequired { get; set; }
    }

    public class IMDatabase : IMObject
    {
        public IMDatabase()
        {

        }

        public IMDatabase(string name, string url)
        {
            Name = name;
            URL = url;
        }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        // None json properties for connection enumeration
        public string Name { get; set; }

        public string URL { get; set; }

        // Return the name as the friendly ToString value
        public override string ToString()
        {
            return Name;
        }
    }

    public class IMCustomField : IMObject
    {
        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        [JsonProperty(PropertyName = "activity_date")]
        public string ActivityDate { get; set; }
    }

    public class IMWarning
    {
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

    }

    public class IMItem : IMObject
    {
        [JsonProperty(PropertyName = "attachment_id")]
        public string AttachmentId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }
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

        [JsonProperty(PropertyName = "custom31")]
        public string Custom31 { get; set; }

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

        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "version")]
        public int Version { get; set; }

        public string conversation_id { get; set; }

        public string conversation_name { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "indexable")]
        public bool Indexable { get; set; }

        [JsonProperty(PropertyName = "is_external")]
        public bool IsExternal { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "retain_days")]
        public int RetainDays { get; set; }

        public List<IMFolder> Recent(K2IMInterface.IMConnection session)
        {
            string url = "workspaces/recent";

            return JsonConvert.DeserializeObject<IMItemList<IMFolder>>(session.PerformGetCall(url)).Data;
        }
    }

    public class IMDocument : IMCutomFieldsObject
    {
        public IMDocument()
        {

        }

        public IMDocument(IMDBObject baseObject)
        {
            Id = baseObject.Id;
            WsType = baseObject.WsType;

            Database = baseObject.Database;
            Name = baseObject.Name;
            Description = baseObject.Description;
            DefaultSecurity = baseObject.DefaultSecurity;
        }

        [JsonProperty(PropertyName = "document_number")]
        public int DocumentNumber { get; set; }

        [JsonProperty(PropertyName = "version")]
        public int version { get; set; }

        [JsonProperty(PropertyName = "alias")]
        public string Alias { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "operator")]
        public string Operator { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "class")]
        public string Clazz { get; set; }

        [JsonProperty(PropertyName = "sub_class")]
        public string SubClass { get; set; }

        [JsonProperty(PropertyName = "edit_date")]
        public string EditDate { get; set; }

        [JsonProperty(PropertyName = "create_date")]
        public string CreateDate { get; set; }

        [JsonProperty(PropertyName = "retain_days")]
        public int RetainDays { get; set; }

        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }

        [JsonProperty(PropertyName = "indexable")]
        public bool Indexable { get; set; }

        [JsonProperty(PropertyName = "is_related")]
        public string IsRelated { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "last_user")]
        public string LastUser { get; set; }

        [JsonProperty(PropertyName = "in_use")]
        public bool InUse { get; set; }

        [JsonProperty(PropertyName = "in_use_by")]
        public string InUseBy { get; set; }

        [JsonProperty(PropertyName = "is_checked_out")]
        public bool IsCheckedOut { get; set; }

        [JsonProperty(PropertyName = "archived")]
        public bool Archived { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "access")]
        public string Access { get; set; }

        [JsonProperty(PropertyName = "checkout_path")]
        public string CheckoutPath { get; set; }

        [JsonProperty(PropertyName = "checkout_due_date")]
        public string CheckoutDueDate { get; set; }

        [JsonProperty(PropertyName = "checkout_comment")]
        public string CheckoutComment { get; set; }

        public string author_description { get; set; }

        public string operator_description { get; set; }

        public string type_description { get; set; }

        public string class_description { get; set; }

        public string sub_class_description { get; set; }

        public string last_user_description { get; set; }

        public string in_use_by_description { get; set; }

        public string edit_time { get; set; }

        [JsonProperty(PropertyName = "extension")]
        public string Extension { get; set; }

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

    public class IMConversation
    {
        public string consersation_id { get; set; }

        public string consersation_name { get; set; }

        public IMDocument[] items { get; set; }
    }

    public class IMEmailParticipant
    {
        public string address { get; set; }

        public string name { get; set; }
    }

    public class IMEmail : IMDocument
    {
        public IMEmail()
        {

        }

        public IMEmail(IMDBObject baseObject)
        {
            Id = baseObject.Id;
            WsType = baseObject.WsType;

            Database = baseObject.Database;
            Name = baseObject.Name;
            Description = baseObject.Description;
            DefaultSecurity = baseObject.DefaultSecurity;
        }

        [JsonProperty(PropertyName = "conversation_id")]
        public string ConversationId { get; set; }

        [JsonProperty(PropertyName = "conversation_name")]
        public string ConversationName { get; set; }

        [JsonProperty(PropertyName = "received_date")]
        public string ReceivedDate { get; set; }

        [JsonProperty(PropertyName = "sent_date")]
        public string SentDate { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "bcc")]
        public IMEmailParticipant[] BCC { get; set; }

        [JsonProperty(PropertyName = "cc")]
        public IMEmailParticipant[] CC { get; set; }

        [JsonProperty(PropertyName = "from")]
        public IMEmailParticipant[] From { get; set; }

        [JsonProperty(PropertyName = "to")]
        public IMEmailParticipant[] To { get; set; }

        public IMConversation Conversation(K2IMInterface.IMConnection session)
        {
            StringBuilder uri = new StringBuilder();

            uri.Append("email/conversation/");
            uri.Append(ConversationId);

            string json = session.PerformGetCall(session.DecorateRESTCall(uri.ToString()));
            if (json.Length > 0)
            {
                return JsonConvert.DeserializeObject<IMConversation>(json);
            }

            return new IMConversation();
        }

        [OnError]
        internal void OnError(System.Runtime.Serialization.StreamingContext context, ErrorContext errorContext)
        {
            errorContext.Handled = true;
        }
    }

    public class IMFolder : IMDBObject
    {
        public IMFolder()
        {

        }

        public IMFolder(IMDBObject baseObject)
        {
            Id = baseObject.Id;
            WsType = baseObject.WsType;

            Database = baseObject.Database;
            Name = baseObject.Name;
            Description = baseObject.Description;
            DefaultSecurity = baseObject.DefaultSecurity;
        }

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

        [JsonProperty(PropertyName = "target")]
        public IMTarget Target { get; set; }

        public string view_type { get; set; }

        public string workspace_id { get; set; }

        public string workspace_name { get; set; }

        public List<IMDocument> Docunments(K2IMInterface.IMConnection session)
        {
            string url = "folders/" + Id + "/documents";

            return JsonConvert.DeserializeObject<IMItemList<IMDocument>>(session.PerformGetCall(url)).Data;
        }

        public List<IMFolder> SubFolders(K2IMInterface.IMConnection session)
        {
            string url = "folders/" + Id + "/subfolders";

            return JsonConvert.DeserializeObject<IMItemList<IMFolder>>(session.PerformGetCall(url)).Data;
        }
    }

    public class IMTemplate : IMCutomFieldsObject
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

        [JsonProperty(PropertyName = "project_custom1")]
        public string ProjectCustom1 { get; set; }

        [JsonProperty(PropertyName = "project_custom2")]
        public string ProjectCustom2 { get; set; }

        [JsonProperty(PropertyName = "project_custom3")]
        public string ProjectCustom3 { get; set; }

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

        [JsonProperty(PropertyName = "operator_description")]
        public string OperatorDescription { get; set; }

        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "version")]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "conversation_id")]
        public string ConversationId { get; set; }

        [JsonProperty(PropertyName = "conversation_name")]
        public string ConversationName { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "indexable")]
        public bool Indexable { get; set; }

        [JsonProperty(PropertyName = "is_external")]
        public bool IsExternal { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "retain_days")]
        public int RetainDays { get; set; }
    }
}
