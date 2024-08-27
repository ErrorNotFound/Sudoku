using Sudoku.Game.Helper;
using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTests.Helper
{
    internal class HighscoreTests
    {
        [Test]
        public void Highscore_AddMoreItemsThanMaxItemSetting_OnlyMaxItemsInList()
        {
            var highscore = new Highscore();

            for(int i = highscore.MaxItemCount + 10; i >= 0; i--)
            {
                highscore.TryAddNewTime(TimeSpan.FromSeconds(i));
            }

            Assert.That(highscore.Items.Count, Is.EqualTo(highscore.MaxItemCount));
        }

        [Test]
        public void Highscore_TryAddSlowerItem_NotAdded()
        {
            var highscore = new Highscore();

            for (int i = 0; i < highscore.MaxItemCount; i++)
            {
                highscore.TryAddNewTime(TimeSpan.FromSeconds(i));
            }

            bool wasAdded = highscore.TryAddNewTime(TimeSpan.FromSeconds(highscore.MaxItemCount));

            Assert.IsFalse(wasAdded);
        }

        [Test]
        public void Highscore_TryAddFasterItem_Added()
        {
            var highscore = new Highscore();

            for (int i = 0; i < highscore.MaxItemCount; i++)
            {
                highscore.TryAddNewTime(TimeSpan.FromSeconds(i));
            }

            bool wasAdded = highscore.TryAddNewTime(TimeSpan.FromSeconds(0));

            Assert.True(wasAdded);
        }
    }
}
