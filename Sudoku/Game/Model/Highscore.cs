using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sudoku.Game.Model
{
    public class Highscore
    {
        public readonly int MaxItemCount = 10;
        private List<TimeSpan> myItems;
        public List<TimeSpan> Items
        {
            get { return myItems; }
            private set { myItems = value; }
        }
        public Highscore()
            :this(new List<TimeSpan>())
        {
        }

        public Highscore(List<TimeSpan> items) 
        {
            Items = items;
        }
        

        public bool TryAddNewTime(TimeSpan time)
        {
            bool added = false;

            if(Items.Count < MaxItemCount)
            {
                Items.Add(time);
                added = true;
            }
            else if(time < Items.Last())
            {
                Items.Remove(Items.Last());
                Items.Add(time);
                added = true;
            }

            if(added)
            {
                Items.Sort();
            }

            return added;
        }

        public static Highscore Load(string filename)
        {
            XmlSerializer xmlSerializer = new(typeof(List<TimeSpan>));
            using (var reader = new StreamReader(filename))
            {
                var temp = xmlSerializer.Deserialize(reader);
                if(temp is List<TimeSpan> e)
                {
                    return new Highscore(e);
                }
                else
                { 
                    throw new Exception("Error while loading highscore"); 
                }
            }
        }

        public void Save(string filename)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<TimeSpan>));
            using (var writer = new StreamWriter(filename))
            {
                xmlSerializer.Serialize(writer, Items);
            }
        }
    }
}
