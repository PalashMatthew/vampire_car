// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("y62daejF4k1twEZx8wxtFaix3TpiOT3TiHpG5r0L+IbS+yDA8Z7T2E71CFVxAQ4XEtLib8mQ8I1aCkuuZ51imN1DAn8jrfa8ZUYbaFWE02RDz/DCSLE71pbLSHRE/XNVziGxKqwTDvEhR3yWD/qi066Qk9+Kb53H4h4zJqkYPAZKLRfgPRy6slhiSrPbfEmo/ira6AXlx2Sx6/NcYvv/cqW1ZSdJcl6wTpu7nkFOkNPpID5cunw5ftwAKyNe+GPVCfK/fI4Sqw25OjQ7C7k6MTm5Ojo7ukeOCwf6Wwu5OhkLNj0yEb1zvcw2Ojo6Pjs49FlV9s0NqFSAdSCUZ0cFhH0eLeTjl5wCjcIrAMcWu8mBcNa9u3wsRRf00AyPPqrMzDk4Ojs6");
        private static int[] order = new int[] { 10,3,8,6,8,9,8,7,11,9,11,11,13,13,14 };
        private static int key = 59;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
