namespace API.POC_MONGO.Infrastructure
{
    public static class Config
    {
        public static class Mongo
        {
            public static string ConnectionString
            {
                get
                {
                    return $"mongodb+srv://{Usuario}:{Senha}@clustercursomongo.cw7w0.mongodb.net?retryWrites=true&w=majority";
                }
            }

            public static string Database
            {
                get
                {
                    return "Curso-Mongo";
                }
            }

            public static string Usuario
            {
                get
                {
                    return "curso-mongo";
                }
            }

            public static string Senha
            {
                get
                {
                    return "curso-mongo";
                }
            }
        }
    }
}