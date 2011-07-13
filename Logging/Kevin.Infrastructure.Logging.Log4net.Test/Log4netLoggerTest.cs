using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net;
namespace Kevin.Infrastructure.Logging.Log4net.Test
{

    [TestClass]
    public class Log4netLoggerTest
    {
        [TestMethod]
        public void Log4netLogger_Create_By_LoggerName_Test()
        {
            //初始化
            ILogger logger = new Log4netLogger("Log4net.Test");


            //操作
            // Log an info level message
            logger.Info("Application [ConsoleApp] Start");

            // Log a debug message. Test if debug is enabled before
            // attempting to log the message. This is not required but
            // can make running without logging faster.
            logger.Debug("This is a debug message");

            try
            {
                Bar();
            }
            catch (Exception ex)
            {
                // Log an error with an exception
                logger.Error("Exception thrown from method Bar", ex);
            }

            logger.Error("Hey this is an error!", null);

            // Push a message on to the Nested Diagnostic Context stack
            //using (log4net.NDC.Push("NDC_Message"))
            //{
            //    log.Warn("This should have an NDC message");

            //    // Set a Mapped Diagnostic Context value  
            //    log4net.MDC.Set("auth", "auth-none");
            //    log.Warn("This should have an MDC message for the key 'auth'");

            //} // The NDC message is popped off the stack at the end of the using {} block

            logger.Warn("See the NDC has been popped of! The MDC 'auth' key is still with us.");

            // Log an info level message
            logger.Info("Application [ConsoleApp] End");

            //查看相应的日志文件验证
        }

        private static void Bar()
        {
            Goo();
        }

        private static void Foo()
        {
            throw new Exception("This is an Exception");
        }

        private static void Goo()
        {
            try
            {
                Foo();
            }
            catch (Exception ex)
            {
                throw new ArithmeticException("Failed in Goo. Calling Foo. Inner Exception provided", ex);
            }
        }
    }
}
