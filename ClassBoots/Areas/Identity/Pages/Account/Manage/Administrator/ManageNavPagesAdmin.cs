using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClassBoots.Areas.Identity.Pages.Account.Manage.Administrator
{
    public static class ManageNavPagesAdmin
    {
        public static string Index => "Index";
        public static string Users => "Users";

        public static string Institutions => "Institutions";

        public static string Schools => "Schools";

        public static string Subjects => "Subjects";

        public static string Lectures => "Lectures";

        public static string Videos => "Videos";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
        public static string UsersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Users);

        public static string InstitutionsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Institutions);

        public static string SchoolsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Schools);

        public static string SubjectsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Subjects);

        public static string LecturesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Lectures);

        public static string VideosNavClass(ViewContext viewContext) => PageNavClass(viewContext, Videos);
   
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
