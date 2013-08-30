using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortfolio.Models.Entities
{
    public partial class UserProfile
    {
        public string PictureUrl
        {
            get
            {
                if (PictureId.HasValue && Picture != null)
                    return string.Format("/File/{0}/{1}", PictureId, Picture.Name);

                return "/Images/account.jpg";
            }
        }
    }
}
