namespace EasMe.Test;

public static class EncryptionTesting
{
    public static void Start() {
        var text = "Hello World";
        Console.WriteLine("Real Text: " + text);
        EasEncrypt.UseTimeSeeding(EasEncrypt.Sensitivity.Minutes);
        EasEncrypt.SetKey("uPgYJAafJnwjbbYGWXakAxZvbcAwFAnGxYtHxLUMvcKWpEdsvkFqcGoQyxcnbGLN");
        var oldEnc = "";
        var oldDec = "";
        for (var i = 0; i < 10000; i++) {
            var em = EasEncrypt.Create();
            var encrypted = em.Encrypt(text);
            var decrypted = em.Decrypt(encrypted);
            if (oldEnc != encrypted || oldDec != decrypted) {
                Console.WriteLine($"{DateTime.UtcNow}  Encrypted: {encrypted}  -  Decrypted: {decrypted}");
            }
            oldEnc = encrypted;
            oldDec = decrypted;
            Thread.Sleep(1000);
        }
    }
}