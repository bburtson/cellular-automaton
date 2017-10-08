using System;

namespace AutomatonJustBecause_Burtson_2017
{
    public class Automaton
    {
        public static readonly int MAX_DISPLAY_WIDTH = 101;

        private readonly bool[] _rules = new bool[8];

        private string _thisGen;

        private char _extremeBit;

        private int _displayWidth;

        public Automaton() { }

        //ctor
        public Automaton(int newRule)
        {
            SetRule(newRule);
            ResetFirstGen();
        }

        public void ResetFirstGen()
        {
            _displayWidth = MAX_DISPLAY_WIDTH;
            _extremeBit = ' ';
            _thisGen = "*";
        }

        public bool SetRule(int newRule)
        {
            //sanitize range
            if (newRule < 0 || newRule > 255) return false;

            //binary conversion allocation
            var cur = newRule;

            for (var i = 0; i <= 7; i++)
            {
                _rules[i] = cur % 2 != 0;

                cur /= 2;
            }

            return true;
        }

        public bool SetDisplayWidth(int width)
        {
            //sanitize width confirm odd
            if (width <= 0 ||
                width >= MAX_DISPLAY_WIDTH ||
                (width & 1) != 1) return false;

            _displayWidth = width;

            return true;
        }

        public string ToStringCurrentGen()
        {
            if (_thisGen.Length == _displayWidth) return _thisGen;

            var smallerIndex = (_displayWidth - _thisGen.Length) / 2;

            var result = new char[_displayWidth];

            if (_thisGen.Length < _displayWidth)
            {
                foreach (var t in _thisGen) result[smallerIndex] = t;

                _thisGen = new string(result);

                return _thisGen;
            }

            var largerIndex = (_thisGen.Length - _displayWidth) / 2;

            _thisGen = _thisGen.Remove(_thisGen.Length - 1 - largerIndex, largerIndex)
                               .Remove(0, largerIndex);

            return _thisGen;
        }

        public void PropagateNewGeneration()
        {
            var doubleBits = $"{_extremeBit}{_extremeBit}";

            _thisGen = _thisGen.Insert(0, doubleBits) + doubleBits;

            var newGen = string.Empty;

            for (var i = 1; i < _thisGen.Length - 1; i++)
            {
                newGen += ApplyRule(_thisGen[i - 1], _thisGen[i], _thisGen[i + 1]);
            }

            _extremeBit = ApplyRule(_extremeBit, _extremeBit, _extremeBit);

            _thisGen = newGen;
        }

        private char ApplyRule(char left, char middle, char right)
        {
            var allPositions = new[] { left, middle, right };

            var ruleValue = 0;

            for (var i = 0; i < 3; i++) if (allPositions[i] == '*') ruleValue += (int)Math.Pow(2, 2 - i);

            return _rules[ruleValue] ? '*' : ' ';
        }
    }
}
