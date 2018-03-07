using NUnit.Framework;
using System;

namespace TechnicalDebtSample.CharacterisationTests
{
    class ExamScorer
    {
        public string GetMessage(int questionNumber, bool isCorrect)
        {
            if (questionNumber == 1)
                return String.Format("1st question {0}", isCorrect ? "is correct" : "is incorrect");
            if (questionNumber == 2)
                return String.Format("2nd question {0}", isCorrect ? "is correct" : "is incorrect");
            if (questionNumber == 3)
                return String.Format("3rd question {0}", isCorrect ? "is correct" : "is incorrect");
            
            return String.Format("{0}th question {1}", questionNumber, isCorrect ? " is correct" : "is incorrect");
        }
    }

    [TestFixture]
    public class ExamScorerTests
    {
        [TestCase(1, true, "1st question is correct")]
        [TestCase(1, false, "1st question is incorrect")]
        [TestCase(2, true, "2nd question is correct")]
        [TestCase(2, false, "2nd question is incorrect")]
        [TestCase(3, true, "3rd question is correct")]
        [TestCase(3, false, "3rd question is incorrect")]
        [TestCase(9, true, "9th question  is correct")] // probably wrong
        [TestCase(9, false, "9th question is incorrect")]
        [TestCase(22, false, "22th question is incorrect")] // definitely wrong        
        //[TestCase(1, true, "sdgfhsdjfhsd")]
        public void CharacterizationTests(int questionNumber, bool isCorrect, string expectedOutput)
        {
            var scorer = new ExamScorer();
            Assert.AreEqual(expectedOutput, scorer.GetMessage(questionNumber, isCorrect));
        }
    }
}
