using ISupportIncrementalLoadingExample.Incremental;
using ISupportIncrementalLoadingExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISupportIncrementalLoadingExample.Services
{
    public class FooService
    {
        private readonly IList<Foo> foos;

        public FooService()
        {
            #region dummy data
            foos = new List<Foo>();
            int idCounter = 1;
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "1 Series",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "2 Series",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "3 Series",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "4 Series",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "5 Series",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "6 Series",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "7 Series",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "i3",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "i8",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "X1",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "X2",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "X3",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "X4",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "X6",
                Category = "BMW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "A1",
                Category = "AUDI"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "A2",
                Category = "AUDI"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "A3",
                Category = "AUDI"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "A4",
                Category = "AUDI"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "A5",
                Category = "AUDI"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "A6",
                Category = "AUDI"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "A8",
                Category = "AUDI"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "A9",
                Category = "AUDI"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "A class",
                Category = "Mercedes"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "C class",
                Category = "Mercedes"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "CLA class",
                Category = "Mercedes"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "E class",
                Category = "Mercedes"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Polo",
                Category = "VW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Golf",
                Category = "VW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Jetta",
                Category = "VW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Passat",
                Category = "VW"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "C1",
                Category = "Citroen"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "C2",
                Category = "Citroen"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "C3",
                Category = "Citroen"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "C4",
                Category = "Citroen"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "C5",
                Category = "Citroen"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "108",
                Category = "Peugeot"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "208",
                Category = "Peugeot"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "308",
                Category = "Peugeot"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "407",
                Category = "Peugeot"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "3008",
                Category = "Peugeot"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "5008",
                Category = "Peugeot"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "508",
                Category = "Peugeot"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Clio",
                Category = "Renault"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Megane",
                Category = "Renault"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Laguna",
                Category = "Renault"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Estate",
                Category = "Renault"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Fabia",
                Category = "Skoda"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Octavia",
                Category = "Skoda"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "SuberB",
                Category = "Skoda"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Yeti",
                Category = "Skoda"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Mio",
                Category = "Seat"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Ibiza",
                Category = "Seat"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Leon",
                Category = "Seat"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Toledo",
                Category = "Seat"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Alhambra",
                Category = "Seat"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Mito",
                Category = "Alfa Romeo"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Brera",
                Category = "Alfa Romeo"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Guilletta",
                Category = "Alfa Romeo"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Bravo",
                Category = "Fiat"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Duplo",
                Category = "Fiat"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Cubo",
                Category = "Fiat"
            });
            foos.Add(new Foo
            {
                ID = idCounter++,
                Name = "Multipla",
                Category = "Fiat"
            });
            #endregion
        }

        public async Task<FooResponse> GetFoos(int page, int items)
        {
            await Task.Delay(1500);//Simulate a network delay
            FooResponse response = new FooResponse();
            var foos = this.foos.Skip(page * items).Take(items).ToList();
            response.Foos = foos;
            var lastFoo = foos.LastOrDefault();
            if (lastFoo == null)
                response.HasMoreItems = false;
            else
            {
                int fooIndex = this.foos.IndexOf(lastFoo);
                if (fooIndex + 1 < this.foos.Count())
                    response.HasMoreItems = true;
            }
            return response;
        }

        public async Task<GroupedFooResponse> GetGroupedFoos(int page, int items)
        {
            GroupedFooResponse response = new GroupedFooResponse();
            var fooResponse = await GetFoos(page, items);
            var grouped = from f in fooResponse.Foos
                          group f by f.Category into g
                          select new GroupModel<String, Foo> { Key = g.Key, Items = g.ToList() };
            response.GroupedFoos = grouped;
            response.HasMoreItems = fooResponse.HasMoreItems;
            return response;
        }
    }
}
