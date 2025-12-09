using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Common;

namespace TrueCounsel.Domain.ValueObjects
{
    public sealed class TagList : ValueObject
    {
        public IReadOnlyList<string> Tags { get; }

        private TagList(IEnumerable<string> tags)
        {
            Tags = tags.Select(t => t.Trim()).Distinct().ToList();
        }

        public static TagList FromCsv(string csv)
        {
            ArgumentNullException.ThrowIfNull(csv);
            return new TagList(csv.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var tag in Tags)
                yield return tag;
        }
    }

}
