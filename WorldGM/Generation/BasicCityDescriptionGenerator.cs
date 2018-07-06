using Rant;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Generation
{
    public class BasicCityDescriptionGenerator : ICityDescriptionGenerator
    {
        public string NextDescription(City city)
        {

            var rant = new RantEngine();
            rant.Do(RantProgram.CompileString($"[vs:region;{city.Region.Name}]"));
            rant.Do(RantProgram.CompileString($"[vs:city;{city.Name}]"));
            rant.Do(RantProgram.CompileString($"[vs:city_of;City of [v:city]]"));
            rant.Do(RantProgram.CompileString($"[vs:pop_rank;second]"));

            string template = @"
                [v:city]{
	                    {
                        {, officially the { Great | }[v:city_of], }
                        |, also known as {
                            the Great\s[v:city_of],
                            | Ye Olde City [v:city],
                            | New [v:city],
                        }
                        }
                        \sis the [v:pop_rank] most populous city in [v:region].
      
                    | \sis a city in the [v:region] region.
                }
            ";

            var output = rant.Do(RantProgram.CompileString(template));

            return output;
        }
    }
}
