using QRGenerator.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;

namespace QRGenerator.Controllers
{
    public class QRCodeGeneratorController : Controller
    {
        SerialPort sp = new SerialPort();
        // GET: QRCodeGenerator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Generate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Generate(QRCodeModel qrcode)
        {
            try
            {
                qrcode.QRCodeImagePath = GenerateQRCode(qrcode.QRCodeText);
                ViewBag.Message = "QR Code created successully";
            }
            catch (Exception e)
            {
                //
            }
            return View("Index",(object)qrcode);
        }

        private string GenerateQRCode(string qrcodeText)
        {
            string folderPath = "~/Images/";
            string imagePath = "~/Images/QRCode.jpg";

            //If the directory doesn't exist then create it
            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(Server.MapPath(folderPath));
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(qrcodeText);

            string barcodePath = Server.MapPath(imagePath);
            var barcodeBitmap = new Bitmap(result);
            using(MemoryStream memory = new MemoryStream())
            {
                using(FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            return imagePath;
        }

        public ActionResult Read()
        {
            return View(ReadQRCode());
        }

        private QRCodeModel ReadQRCode()
        {
            QRCodeModel barcodeModel = new QRCodeModel();
            string barcodeText = "";
            string imagePath = "~/Images/QRCode.jpg";
            string barcodePath = Server.MapPath(imagePath);
            var barcodeReader = new BarcodeReader();

            var result = barcodeReader.Decode(new Bitmap(barcodePath));
            if(result != null)
            {
                barcodeText = result.Text;
            }
            return new QRCodeModel() { QRCodeText = barcodeText, QRCodeImagePath = imagePath };
        }

        public ActionResult MobileQR()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MobileQR(QRCodeModel qrcode)
        {
            qrcode.QRCodeImagePath = GenerateQRCode(qrcode.QRCodeText);
            ViewBag.Message = "QR Code created successully";
            return View("Index", (object)qrcode);
        }

        public ActionResult EmailQR()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmailQR(QRCodeModel qrcode)
        {
            qrcode.QRCodeImagePath = GenerateQRCode(qrcode.QRCodeText);
            ViewBag.Message = "QR Code created successully";
            return View("Index", (object)qrcode);
        }

        public ActionResult Whatsapp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Whatsapp(QRCodeModel qrcode)
        {
            qrcode.QRCodeImagePath = GenerateQRCode(qrcode.QRCodeText);
            ViewBag.Message = "QR Code created successully";
            return View("Index", (object)qrcode);
        }

        public ActionResult SMSIndex()
        {
            return View();
        }

        public ActionResult SendSMS()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult SendSMS(QRCodeModel model)
        {
            try
            {
                SendSMS(model.PhoneNumber, model.Message);
                System.Threading.Thread.Sleep(3000);
                SendSMS(model.PhoneNumber, model.Message);
                return RedirectToAction("SMSIndex");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void SendSMS(string mobNo, string msg)
        {
            string telno = char.ConvertFromUtf32(34) + mobNo + char.ConvertFromUtf32(34);

            sp.PortName = "COM14";
            sp.Open();
            sp.Write("AT+CMGF=1" + char.ConvertFromUtf32(13));
            sp.Write("AT+CMGS=" + telno + char.ConvertFromUtf32(13));
            sp.Write(msg + char.ConvertFromUtf32(26) + char.ConvertFromUtf32(13));
            sp.Close();
        }

    }
}