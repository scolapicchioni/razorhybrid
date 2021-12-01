using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSharingApplication.Core.Entities {
    public class Comment {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int PhotoId { get; set; }

        public int Max(Func<object, object> p) {
            throw new NotImplementedException();
        }
    }
}
