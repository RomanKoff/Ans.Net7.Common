namespace Ans.Net7.Common
{

    public static partial class _Consts
	{

		public static readonly Dictionary<string, string> HTML_MACROS = new()
		{
			{ "ndash;", "–" }, // EN DASH
			{ "mdash;", "—" }, // EM DASH
			{ "oline;", "‾" }, // OVERLINE
			{ "lsquo;", "‘" }, // LEFT SINGLE QUOTATION MARK
			{ "rsquo;", "’" }, // RIGHT SINGLE QUOTATION MARK
			{ "sbquo;", "‚" }, // SINGLE LOW-9 QUOTATION MARK
			{ "ldquo;", "“" }, // LEFT DOUBLE QUOTATION MARK
			{ "rdquo;", "”" }, // RIGHT DOUBLE QUOTATION MARK
			{ "bdquo;", "„" }, // DOUBLE LOW-9 QUOTATION MARK
			{ "prime;", "′" }, // PRIME
			{ "Prime;", "″" }, // DOUBLE PRIME
			{ "lsaquo;", "‹" }, // SINGLE LEFT-POINTING ANGLE QUOTATION MARK
			{ "rsaquo;", "›" }, // SINGLE RIGHT-POINTING ANGLE QUOTATION MARK
			{ "laquo;", "«" },
			{ "raquo;", "»" },
			
			{ "tilde;", "˜" },
			{ "uml;", "¨" },
			{ "dagger;", "†" }, // DAGGER
			{ "Dagger;", "‡" }, // DOUBLE DAGGER
			
			{ "middot;", "·" },
			{ "bull;", "•" }, // BULLET
			{ "hellip;", "…" }, // HORIZONTAL ELLIPSIS
			
			{ "sect;", "§" },
			{ "para;", "¶" },
			{ "permil;", "‰" }, // PER MILLE SIGN
			
			{ "brvbar;", "¦" },
			{ "circ;", "ˆ" },
			{ "iexcl;", "¡" },
			{ "iquest;", "¿" },
			{ "macr;", "¯" },
			{ "cedil;", "¸" },
			{ "sdot;", "⋅" }, // DOT OPERATOR
			{ "acute;", "´" },
			
			//-- Currency Symbols
			{ "euro;", "€" }, // EURO SIGN
			{ "pound;", "£" },
			{ "rur;", "₽" },
			{ "yen;", "¥" },
			{ "cent;", "¢" },
			{ "curren;", "¤" },
			
			//-- Letterlike Symbols
			{ "reg;", "®" },
			{ "copy;", "©" },
			{ "trade;", "™" }, // TRADE MARK SIGN
			{ "image;", "ℑ" }, // BLACK-LETTER CAPITAL I
			{ "weierp;", "℘" }, // SCRIPT CAPITAL P
			{ "real;", "ℜ" }, // BLACK-LETTER CAPITAL R				
			{ "ohm;", "Ω" }, // OHM SIGN
			{ "mho;", "℧" }, // INVERTED OHM SIGN
			{ "alefsym;", "ℵ" }, // ALEF SYMBOL
			
			//-- Arrows
			{ "larr;", "←" }, // LEFTWARDS ARROW
			{ "uarr;", "↑" }, // UPWARDS ARROW
			{ "rarr;", "→" }, // RIGHTWARDS ARROW
			{ "darr;", "↓" }, // DOWNWARDS ARROW
			{ "harr;", "↔" }, // LEFT RIGHT ARROW
			{ "lArr;", "⇐" }, // LEFTWARDS DOUBLE ARROW
			{ "uArr;", "⇑" }, // UPWARDS DOUBLE ARROW
			{ "rArr;", "⇒" }, // RIGHTWARDS DOUBLE ARROW
			{ "dArr;", "⇓" }, // DOWNWARDS DOUBLE ARROW
			{ "hArr;", "⇔" }, // LEFT RIGHT DOUBLE ARROW
			{ "crarr;", "↵" }, // DOWNWARDS ARROW WITH CORNER LEFTWARDS
			
			//-- Mathematical Operators
			{ "minus;", "−" }, // MINUS SIGN
			{ "plusmn;", "±" },
			{ "times;", "×" },
			{ "frasl;", "⁄" }, // FRACTION SLASH
			{ "divide;", "÷" },
			{ "lowast;", "∗" }, // ASTERISK OPERATOR
			{ "int;", "∫" }, // INTEGRAL
			{ "radic;", "√" }, // SQUARE ROOT
			{ "asymp;", "≈" }, // ALMOST EQUAL TO
			{ "ne;", "≠" }, // NOT EQUAL TO
			{ "equiv;", "≡" }, // IDENTICAL TO
			{ "sim;", "∼" }, // TILDE OPERATOR
			{ "cong;", "≅" }, // APPROXIMATELY EQUAL TO
			{ "there4;", "∴" }, // THEREFORE
			{ "le;", "≤" }, // LESS-THAN OR EQUAL TO
			{ "ge;", "≥" }, // GREATER-THAN OR EQUAL TO
			{ "deg;", "°" },
			{ "sup1;", "¹" },
			{ "sup2;", "²" },
			{ "sup3;", "³" },
			{ "frac12;", "½" },
			{ "frac14;", "¼" },
			{ "frac34;", "¾" },
			{ "ordf;", "ª" },
			{ "ordm;", "º" },
			{ "prod;", "∏" }, // N-ARY PRODUCT
			{ "sum;", "∑" }, // N-ARY SUMMATION
			{ "part;", "∂" }, // PARTIAL DIFFERENTIAL				
			{ "forall;", "∀" }, // FOR ALL
			{ "exist;", "∃" }, // THERE EXISTS
			{ "nabla;", "∇" }, // NABLA
			{ "and;", "∧" }, // LOGICAL AND
			{ "or;", "∨" }, // LOGICAL OR
			{ "not;", "¬" },
			{ "cap;", "∩" }, // INTERSECTION
			{ "cup;", "∪" }, // UNION				
			{ "sub;", "⊂" }, // SUBSET OF
			{ "isin;", "∈" }, // ELEMENT OF
			{ "sube;", "⊆" }, // SUBSET OF OR EQUAL TO
			{ "nsub;", "⊄" }, // NOT A SUBSET OF				
			{ "notin;", "∉" }, // NOT AN ELEMENT OF
			{ "sup;", "⊃" }, // SUPERSET OF				
			{ "ni;", "∋" }, // CONTAINS AS MEMBER
			{ "supe;", "⊇" }, // SUPERSET OF OR EQUAL TO
			{ "empty;", "∅" }, // EMPTY SET
			{ "oplus;", "⊕" }, // CIRCLED PLUS
			{ "otimes;", "⊗" }, // CIRCLED TIMES
			{ "prop;", "∝" }, // PROPORTIONAL TO
			{ "infin;", "∞" }, // INFINITY
			{ "ang;", "∠" }, // ANGLE
			{ "perp;", "⊥" }, // UP TACK				
			{ "fnof;", "ƒ" },
			{ "Oslash;", "Ø" },
			{ "pi;", "π" },
			{ "micro;", "µ" },
			{ "loz;", "◊" }, // LOZENGE
			
			{ "spades;", "♠" }, // BLACK SPADE SUIT
			{ "clubs;", "♣" }, // BLACK CLUB SUIT
			{ "hearts;", "♥" }, // BLACK HEART SUIT
			{ "diams;", "♦" }, // BLACK DIAMOND SUIT
		};

	}

}
