using System;
using System.Text.RegularExpressions;

namespace NameSearch.Models.Request.Domain
{
    public class SearchModel
    {
        private string _where;

        public string LastName
        {
            get
            {

				if (!string.IsNullOrWhiteSpace(Name) && (Name.Contains(" ")))
                {
                    return Name.Substring(Name.IndexOf(" ", StringComparison.InvariantCultureIgnoreCase) + 1);
                }
                return Name;
            }
        }

        public string FirstName
        {
            get
            {
				if (!string.IsNullOrWhiteSpace(Name) && (Name.Contains(" ")))
                {
                    return Name.Substring(0, Name.IndexOf(" ", StringComparison.InvariantCultureIgnoreCase));
                }
                return Name;
            }
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Where
        {
            get => _where;
            set
            {
                _where = value;
                string city;
                string state;
                var zip = _where;
                var pat = "^\\d.*";
                if (Regex.IsMatch(_where, pat))
                {
                    PostalCode = _where.Trim();
                    state = "";
                    city = "";
                }
                else
                {
                    // do we have a state?
                    pat = ".*\\s*(?<state>[A-Za-z]{2})\\s*.*";
                    if (Regex.IsMatch(_where, pat))
                    {
						var wlen = _where.Length;
                        var regex = new Regex(pat);
                        var match = regex.Match(_where);
                        state = match.Groups["state"].Value;
                        var index = _where.IndexOf(state, StringComparison.InvariantCultureIgnoreCase);
                        city = _where.Substring(0, index - 1).Trim();
						city = city.TrimEnd(' ', ',');
						var proposedZipStart = index + state.Length;
						zip = proposedZipStart > wlen ? _where.Substring(proposedZipStart).Trim() : "";
                    }
                    else
                    {
                        // no state
                        // is there a numeric zip code at the end?
                        pat = ".*(?<zip>\\d{5})\\s*$";
                        if (Regex.IsMatch(_where, pat))
                        {
                            var regex = new Regex(pat);
                            var match = regex.Match(_where);
                            zip = match.Groups["zip"].Value;
                            var idx = _where.IndexOf(zip, StringComparison.InvariantCultureIgnoreCase);
                            city = _where.Substring(0, idx - 1);
                            state = "";
                        }
                        else
                        {
                            // no state, no zip: all is city
                            city = _where;
                            state = "";
                            zip = "";
                        }
                    }

                }
                City = city;
                State = state;
                PostalCode = zip;
            }
        }

        public string City { get; private set; }

        public string State { get; private set; }

        public string PostalCode { get; private set; }
    }
}
