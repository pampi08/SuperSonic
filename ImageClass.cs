using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using AForge.Imaging;
using AForge.Math.Geometry;
using AForge;
using AForge.Imaging.Filters;

namespace SS_OpenCV
{
    class ImageClass
    {

        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        public static void Negative(Image<Bgr, byte> img)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;
                int nblue, ngreen, nred;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to negative
                            nblue = 255 - blue;
                            ngreen = 255 - green;
                            nred = 255 - red;

                            // store in the image
                            dataPtr[0] = (byte)nblue;
                            dataPtr[1] = (byte)ngreen;
                            dataPtr[2] = (byte)nred;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)(((int)blue + green + red) / 3);

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgUndo, int dx, int dy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgUndo.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();



                int xorigem, yorigem, xdest, ydest;



                if (nChan == 3)
                {
                    for (ydest = 0; ydest < height; ydest++)
                    {
                        for (xdest = 0; xdest < width; xdest++)
                        {
                            xorigem = xdest - dx;
                            yorigem = ydest - dy;


                            if (xorigem >= 0 && yorigem >= 0 && xorigem < width && yorigem < height)
                            {
                                dataPtrAux = (yorigem * widthStep + xorigem * nChan + dataPtrOrigem);
                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];

                            }
                            else
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }
                            dataPtr += nChan;
                            // acesso directo : mais lento 
                            // aux = img[y, x];
                            //img[y, x] = new Bgr(255 - aux.Blue, 255 - aux.Green, 255 - aux.Red);
                        }

                        dataPtr += padding;
                    }

                }
            }
        }

        

        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgUndo, float radian)
        {

            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgUndo.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();


                int xorigem, yorigem, xdest, ydest;
                double cX = width / 2.0;
                double cY = height / 2.0;
                double senR = Math.Sin(radian);
                double cosR = Math.Cos(radian);

                if (nChan == 3)
                {
                    for (ydest = 0; ydest < height; ydest++)
                    {
                        for (xdest = 0; xdest < width; xdest++)
                        {
                            xorigem = (int)Math.Round((xdest - cX) * cosR - (cY - ydest) * senR + cX);
                            yorigem = (int)Math.Round(cY - (xdest - cX) * senR - (cY - ydest) * cosR);


                            if (xorigem >= 0 && yorigem >= 0 && xorigem < width && yorigem < height)
                            {
                                dataPtrAux = (yorigem * widthStep + xorigem * nChan + dataPtrOrigem);
                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];
                            }
                            else
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }


                            dataPtr += nChan;

                        }

                        dataPtr += padding;
                    }
                }
            }

        }

        public static void Rotation_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgUndo, float radian)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgUndo.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();

                double xorigem, yorigem, xdest, ydest, dx, dy, b1B, b1G, b1R, b2B, b2G, b2R;
                int j, k, jkB, jkplus1B, jplus1kB, jplus1kplus1B,
                    jkG, jkplus1G, jplus1kG, jplus1kplus1G,
                    jkR, jkplus1R, jplus1kR, jplus1kplus1R, btotalB, btotalG, btotalR;
                
                double cX = width / 2.0;
                double cY = height / 2.0;
                double senR = Math.Sin(radian);
                double cosR = Math.Cos(radian);

                if (nChan == 3)
                {
                    for (ydest = 0; ydest < height; ydest++)
                    {
                        for (xdest = 0; xdest < width; xdest++)
                        {
                            xorigem = (int)Math.Round((xdest - cX) * cosR - (cY - ydest) * senR + cX);
                            yorigem = (int)Math.Round(cY - (xdest - cX) * senR - (cY - ydest) * cosR);

                            if (xorigem >= 0 && yorigem >= 0 && xorigem < width && yorigem < height)
                            {
                                j = (int)Math.Floor(yorigem);
                                k = (int)Math.Floor(xorigem);

                                dx = xorigem - j;
                                dy = yorigem - k;

                                if (dx > 0 || dy > 0)
                                {
                                    dataPtrAux = (j * widthStep + k * nChan + dataPtrOrigem);
                                    jkB = dataPtrAux[0];
                                    jkG = dataPtrAux[1];
                                    jkR = dataPtrAux[2];
                                    dataPtrAux = ((j + 1) * widthStep + k * nChan + dataPtrOrigem);
                                    jplus1kB = dataPtrAux[0];
                                    jplus1kG = dataPtrAux[1];
                                    jplus1kR = dataPtrAux[2];
                                    dataPtrAux = (j * widthStep + (k + 1) * nChan + dataPtrOrigem);
                                    jkplus1B = dataPtrAux[0];
                                    jkplus1G = dataPtrAux[1];
                                    jkplus1R = dataPtrAux[2];
                                    dataPtrAux = ((j + 1) * widthStep + (k + 1) * nChan + dataPtrOrigem);
                                    jplus1kplus1B = dataPtrAux[0];
                                    jplus1kplus1G = dataPtrAux[1];
                                    jplus1kplus1R = dataPtrAux[2];

                                    b1B = Math.Round((1 - dx) * jkB + dx * jkplus1B);
                                    b2B = Math.Round((1 - dx) * jplus1kB + dx * jplus1kplus1B);
                                    btotalB = (int)Math.Round((1 - dy) * b1B + dy * b2B);
                                    b1G = Math.Round((1 - dx) * jkG + dx * jkplus1G);
                                    b2G = Math.Round((1 - dx) * jplus1kG + dx * jplus1kplus1G);
                                    btotalG = (int)Math.Round((1 - dy) * b1G + dy * b2G);
                                    b1R = Math.Round((1 - dx) * jkR + dx * jkplus1R);
                                    b2R = Math.Round((1 - dx) * jplus1kR + dx * jplus1kplus1R);
                                    btotalR = (int)Math.Round((1 - dy) * b1R + dy * b2R);

                                    if (btotalB < 0) { btotalB = 0; }
                                    if (btotalG < 0) { btotalG = 0; }
                                    if (btotalR < 0) { btotalR = 0; }
                                    if (btotalB > 255) { btotalB = 255; }
                                    if (btotalG > 255) { btotalG = 255; }
                                    if (btotalR > 255) { btotalR = 255; }
                                    dataPtr[0] = (byte)btotalB;
                                    dataPtr[1] = (byte)btotalG;
                                    dataPtr[2] = (byte)btotalR;
                                }
                            }
                            else
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }

                        dataPtr += nChan;

                        }

                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgUndo)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgUndo.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();

                //int N = 3;
                int xdest = 0;
                int ydest = 0;
                //int xorigem, yorigem;
                uint sumB = 0;
                uint sumG = 0;
                uint sumR = 0;

                double sumCB = 0;
                double sumCG = 0;
                double sumCR = 0;
                
                double sumM1B = 0;
                double sumM1G = 0;
                double sumM1R = 0;
                double sumM2B = 0;     //soma das componentes nas margens
                double sumM2G = 0;
                double sumM2R = 0;
                double sumM3B = 0;
                double sumM3G = 0;
                double sumM3R = 0;
                double sumM4B = 0;
                double sumM4G = 0;
                double sumM4R = 0;

                double mediaB = 0;
                double mediaG = 0;
                double mediaR = 0;
                double counterSum = 0;

                // ------------------------------------------|||---------------------------------------
                //PROCESSAR O INTERIOR S/ MARGENS
                if (nChan == 3)
                {
                    dataPtr += nChan + widthStep; //pointer da imagem no ponto (1,1)

                    for (ydest = 1; ydest < height-1; ydest++)
                    {
                        
                        for (xdest = 1; xdest < width-1; xdest++)
                        {
                            for (int i = ydest - 1; i <= ydest + 1; i++)
                            {
                                for (int j = xdest - 1; j <= xdest + 1; j++)
                                {
                                    dataPtrAux = (i * widthStep + j * nChan + dataPtrOrigem);

                                    sumB += dataPtrAux[0];
                                    sumG += dataPtrAux[1];
                                    sumR += dataPtrAux[2];
                              
                                    
                                    counterSum++;
                                }
                            }
                            mediaB = Math.Round(sumB /counterSum);
                            mediaG = Math.Round(sumG /counterSum);
                            mediaR = Math.Round(sumR / counterSum);

                            sumB = 0;
                            sumG = 0;
                            sumR = 0;

                            dataPtr[0] = (byte)mediaB;
                            dataPtr[1] = (byte)mediaG;
                            dataPtr[2] = (byte)mediaR;
                            dataPtr += nChan; //termina no pixel (width-1,height-2)
                            counterSum = 0;
                        }

                        dataPtr += 2 * nChan + padding; //termina no pixel (1,height-1) CHECK
                        
                    }


                    // ------------------------------------------|||---------------------------------------
                    // PROCESSAR OS CANTOS
                    // 1º CANTO (0,0)

                    dataPtr -= nChan + (height - 1) * widthStep;        //Pointer na imagem de origem no pixel (0,0) CHECK
                    dataPtrAux = dataPtrOrigem;     //Pointer para calculo da média no pixel (0,0)

                    sumCB = 4 * dataPtrAux[0];
                    sumCG = 4 * dataPtrAux[1];
                    sumCR = 4 * dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (1,0)

                    sumCB += 2 * dataPtrAux[0];
                    sumCG += 2 * dataPtrAux[1];
                    sumCR += 2 * dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;     //Pointer no pixel (0,1)

                    sumCB += 2 * dataPtrAux[0];
                    sumCG += 2 * dataPtrAux[1];
                    sumCR += 2 * dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (1,1)

                    sumCB += dataPtrAux[0];
                    sumCG += dataPtrAux[1];
                    sumCR += dataPtrAux[2];

                    dataPtr[0] = (byte)(Math.Round(sumCB / 9));
                    dataPtr[1] = (byte)(Math.Round(sumCG / 9));
                    dataPtr[2] = (byte)(Math.Round(sumCR / 9));

                    
                    // ------------------------------------------|||---------------------------------------
                    // 2º CANTO (width-1,0)

                    dataPtr += (width - 1) * nChan;     //Pointer na imagem principal no pixel (width-1,0)
                    dataPtrAux = (width - 2) * nChan + dataPtrOrigem; //Pointer no pixel (width-2, 0)

                    sumCB = 2 * dataPtrAux[0];
                    sumCG = 2 * dataPtrAux[1];
                    sumCR = 2 * dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (width-1,0)

                    sumCB += 4 * dataPtrAux[0];
                    sumCG += 4 * dataPtrAux[1];
                    sumCR += 4 * dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;     //Pointer no pixel (width-2,1)

                    sumCB += dataPtrAux[0];
                    sumCG += dataPtrAux[1];
                    sumCR += dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (width-1,1)

                    sumCB += 2 * dataPtrAux[0];
                    sumCG += 2 * dataPtrAux[1];
                    sumCR += 2 * dataPtrAux[2];

                    dataPtr[0] = (byte)(Math.Round(sumCB / 9));
                    dataPtr[1] = (byte)(Math.Round(sumCG / 9));
                    dataPtr[2] = (byte)(Math.Round(sumCR / 9));

                    // ------------------------------------------|||---------------------------------------
                    // 3º CANTO (width-1,height-1)

                    dataPtr += (height - 1) * widthStep;     //Pointer na imagem principal no pixel (width-1,height-1)
                    dataPtrAux = ((height - 2) * widthStep) + ((width - 2) * nChan) + dataPtrOrigem; //Pointer no pixel (width-2, height-2)

                    sumCB = dataPtrAux[0];
                    sumCG = dataPtrAux[1];
                    sumCR = dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (width-1,height-2)

                    sumCB += 2 * dataPtrAux[0];
                    sumCG += 2 * dataPtrAux[1];
                    sumCR += 2 * dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;     //Pointer no pixel (width-2,height-1)

                    sumCB += 2 * dataPtrAux[0];
                    sumCG += 2 * dataPtrAux[1];
                    sumCR += 2 * dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (width-1,height-1)

                    sumCB += 4 * dataPtrAux[0];
                    sumCG += 4 * dataPtrAux[1];
                    sumCR += 4 * dataPtrAux[2];

                    dataPtr[0] = (byte)(Math.Round(sumCB / 9));
                    dataPtr[1] = (byte)(Math.Round(sumCG / 9));
                    dataPtr[2] = (byte)(Math.Round(sumCR / 9));


                    // ------------------------------------------|||---------------------------------------
                    // 4º CANTO (0,height-1)

                    dataPtr -= (width - 1) * nChan;     //Pointer na imagem principal no pixel (0, height-1)
                    dataPtrAux = (height - 2) * widthStep + dataPtrOrigem; //Pointer no pixel (0, height-2)

                    sumCB = 2 * dataPtrAux[0];
                    sumCG = 2 * dataPtrAux[1];
                    sumCR = 2 * dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (1, height-2)

                    sumCB += dataPtrAux[0];
                    sumCG += dataPtrAux[1];
                    sumCR += dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;     //Pointer no pixel (0, height-1)

                    sumCB += 4 * dataPtrAux[0];
                    sumCG += 4 * dataPtrAux[1];
                    sumCR += 4 * dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (1, height-1)

                    sumCB += 2 * dataPtrAux[0];
                    sumCG += 2 * dataPtrAux[1];
                    sumCR += 2 * dataPtrAux[2];

                    dataPtr[0] = (byte)(Math.Round(sumCB / 9));
                    dataPtr[1] = (byte)(Math.Round(sumCG / 9));
                    dataPtr[2] = (byte)(Math.Round(sumCR / 9));

                    //Pointer na imagem principal no pixel (0, height-1)
                    

                    // ------------------------------------------|||---------------------------------------

                    // AGORA PROCESSAMOS AS MARGENS S/ CANTOS
                    // Margem Superior(M1)
                    dataPtr -= ((height - 1) * widthStep) - nChan;       //Pointer na imagem principal no pixel (1,0)


                    for (xdest = 1; xdest < width - 1; xdest++) //Pointer ao longo da linha y=0
                    {

                        for (int j = xdest - 1; j < xdest + 2; j++) //ir ao pixel antes e depois do pixel a ser processado
                        {
                            dataPtrAux = (j * nChan + dataPtrOrigem);

                            sumM1B += 2 * dataPtrAux[0];
                            sumM1G += 2 * dataPtrAux[1];
                            sumM1R += 2 * dataPtrAux[2];


                            dataPtrAux = (widthStep + j * nChan + dataPtrOrigem); //linha seguinte ao pixel a ser processado

                            sumM1B += dataPtrAux[0];
                            sumM1G += dataPtrAux[1];
                            sumM1R += dataPtrAux[2];
                        }
                        sumM1B = Math.Round(sumM1B / 9);
                        sumM1G = Math.Round(sumM1G / 9);
                        sumM1R = Math.Round(sumM1R / 9);

                        dataPtr[0] = (byte)(sumM1B);
                        dataPtr[1] = (byte)(sumM1G);
                        dataPtr[2] = (byte)(sumM1R);

                        sumM1B = 0;
                        sumM1G = 0;
                        sumM1R = 0;

                        dataPtr += nChan; //termina quando dataPtr = (width-1 ,0) CHECK
                        
                    }
                    // ------------------------------------------|||---------------------------------------
                    // Margem Lateral Direita(M2)
                    dataPtr += widthStep;      //Pointer na imagem principal no pixel (width-1,1)


                    for (ydest = 1; ydest < height - 1; ydest++) //Pointer ao longo da coluna x=width-1
                    {

                        for (int j = ydest - 1; j < ydest + 2; j++) //ir ao pixel acima e abaixo do pixel a ser processado
                        {
                            dataPtrAux = ((width - 1) * nChan + j * widthStep + dataPtrOrigem);

                            sumM2B += 2 * dataPtrAux[0];
                            sumM2G += 2 * dataPtrAux[1];
                            sumM2R += 2 * dataPtrAux[2];


                            dataPtrAux = ((width - 2) * nChan + j * widthStep + dataPtrOrigem); //coluna anterior ao pixel a ser processado

                            sumM2B += dataPtrAux[0];
                            sumM2G += dataPtrAux[1];
                            sumM2R += dataPtrAux[2];
                        }
                        sumM2B = Math.Round(sumM2B / 9);
                        sumM2G = Math.Round(sumM2G / 9);
                        sumM2R = Math.Round(sumM2R / 9);

                        dataPtr[0] = (byte)(sumM2B);
                        dataPtr[1] = (byte)(sumM2G);
                        dataPtr[2] = (byte)(sumM2R);

                        sumM2B = 0;
                        sumM2G = 0;
                        sumM2R = 0;

                        dataPtr += widthStep; //temina quando dataPtr = (width-1, height-1)
                    }
                    //// Margem Inferior (M3)
                    dataPtr -= (width - 2) * nChan;       //Pointer na imagem principal no pixel (1,height-1)


                    for (xdest = 1; xdest < width - 1; xdest++) //Pointer ao longo da linha y=height
                    {

                        for (int j = xdest - 1; j < xdest + 2; j++) //ir ao pixel antes e depois do pixel a ser processado
                        {
                            dataPtrAux = ((height - 1) * widthStep + j * nChan + dataPtrOrigem);

                            sumM3B += 2 * dataPtrAux[0];
                            sumM3G += 2 * dataPtrAux[1];
                            sumM3R += 2 * dataPtrAux[2];


                            dataPtrAux = ((height - 2) * widthStep + j * nChan + dataPtrOrigem); //linha acima do pixel a ser processado

                            sumM3B += dataPtrAux[0];
                            sumM3G += dataPtrAux[1];
                            sumM3R += dataPtrAux[2];
                        }
                        sumM3B = Math.Round(sumM3B / 9);
                        sumM3G = Math.Round(sumM3G / 9);
                        sumM3R = Math.Round(sumM3R / 9);

                        dataPtr[0] = (byte)(sumM3B);
                        dataPtr[1] = (byte)(sumM3G);
                        dataPtr[2] = (byte)(sumM3R);

                        sumM3B = 0;
                        sumM3G = 0;
                        sumM3R = 0;

                        dataPtr += nChan; //termina quando dataPtr = (width-1 ,height-1) check

                    }
                    // ------------------------------------------|||---------------------------------------
                    // Margem Lateral Esquerda (M4)
                    dataPtr -= ((width - 1) * nChan + (height - 2) * widthStep);      //Pointer na imagem principal no pixel (0,1)


                    for (ydest = 1; ydest < height - 1; ydest++) //Pointer ao longo da coluna x=0
                    {

                        for (int j = ydest - 1; j < ydest + 2; j++) //ir ao pixel acima e abaixo do pixel a ser processado
                        {
                            dataPtrAux = (j * widthStep + dataPtrOrigem);

                            sumM4B += 2 * dataPtrAux[0];
                            sumM4G += 2 * dataPtrAux[1];
                            sumM4R += 2 * dataPtrAux[2];

                            dataPtrAux = (j * widthStep + nChan + dataPtrOrigem); //coluna seguinte ao pixel a ser processado

                            sumM4B += dataPtrAux[0];
                            sumM4G += dataPtrAux[1];
                            sumM4R += dataPtrAux[2];
                        }

                        sumM4B = Math.Round(sumM4B / 9);
                        sumM4G = Math.Round(sumM4G / 9);
                        sumM4R = Math.Round(sumM4R / 9);

                        dataPtr[0] = (byte)(sumM4B);
                        dataPtr[1] = (byte)(sumM4G);
                        dataPtr[2] = (byte)(sumM4R);

                        sumM4B = 0;
                        sumM4G = 0;
                        sumM4R = 0;

                        dataPtr += widthStep;

                    }

                }

            }
        }

        public static void Chess_Recognition(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, 
            out Rectangle BD_Location, out string Angle, out string[,] Pieces)
        {
            unsafe
            {
                Roberts(img, imgCopy);
                ConvertToBW(img, 20);
                MIplImage m = img.MIplImage;
                //MIplImage morigem = imgUndo.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;
                //byte* dataPtrAux;
                //byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int x0 = 0;
                int y0 = 0;
                int x1 = 0;
                int y1 = 0;

                Boolean xbreak = false;
                bool canto1 = false;
                bool canto2 = false;
                int chessWidth = 0;

                if (nChan == 3) // image in RGB
                {
                    

                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            dataPtr[0] = 0;
                            dataPtr[1] = 0;
                            dataPtr[2] = 0;

                            if (!canto1 && blue >= 250 && green >= 250 && green >= 250)
                            {
                                x0 = x;
                                y0 = y;
                                canto1 = true;
                                
                                
                            } if(canto1 && !canto2)
                            {
                                chessWidth++;
                            }    
                            
                            if (canto1 && blue <= 10 && green <= 10 && green <= 10)
                            {
                                canto2 = true;
                                x1 = x;
                                y1 = y;
                                xbreak = true;
                                break;
                            }
                           
                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                            
                        }
                        if (xbreak)
                        {
                            xbreak = false;
                            break;
                        }
                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                    Debug.WriteLine(x0 + " " + y0 + " " + x1 + " " + y1 + " " + chessWidth + " " + (x1-x0));
                    //chessWidth = x1 - y1;
                }
                   
                    string[,] tmp = new string[8, 8];

                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            tmp[j, i] = "K_w";
                        }
                    }
               
                Pieces = tmp;
                Angle = 48.ToString();
                BD_Location = new Rectangle(x0, y0, chessWidth-2, 1000);
            }
            
        }

        public static void Mean_solutionB(Image<Bgr, byte> img, Image<Bgr, byte> imgUndo)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgUndo.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();

                //int N = 3;
                int xdest = 0;
                int ydest = 0;
                //int xorigem, yorigem;
                uint sumB = 0;
                uint sumG = 0;
                uint sumR = 0;
                uint sum1B = 0;
                uint sum1G = 0;
                uint sum1R = 0;

                double sumCB = 0;
                double sumCG = 0;
                double sumCR = 0;

                double sumM1B = 0;
                double sumM1G = 0;
                double sumM1R = 0;
                double sumM2B = 0;     //soma das componentes nas margens
                double sumM2G = 0;
                double sumM2R = 0;
                double sumM3B = 0;
                double sumM3G = 0;
                double sumM3R = 0;
                double sumM4B = 0;
                double sumM4G = 0;
                double sumM4R = 0;

                double mediaB = 0;
                double mediaG = 0;
                double mediaR = 0;
                double counterSum = 0;

                if (nChan == 3)
                {
                    // ------------------------------------------|||---------------------------------------
                    //CALCULAR A MÉDIA PARA O 1º PIXEL DO INTERIOR

                    dataPtrAux = dataPtrOrigem;     //Pointer para calculo da média no pixel (0,0)

                    sumB = dataPtrAux[0];
                    sumG = dataPtrAux[1];
                    sumR = dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (1,0)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (2,0)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    dataPtrAux += widthStep - 2*nChan;     //Pointer no pixel (0,1)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (1,1)

                    sumCB += dataPtrAux[0];
                    sumCG += dataPtrAux[1];
                    sumCR += dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (2,1)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    dataPtrAux += widthStep - 2 * nChan;     //Pointer no pixel (0,2)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (1,2)

                    sumCB += dataPtrAux[0];
                    sumCG += dataPtrAux[1];
                    sumCR += dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (2,2)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    mediaB = sumB / 9.0;
                    mediaG = sumG / 9.0;
                    mediaR = sumR / 9.0;

                    if (mediaB > 255) { mediaB = 255; }
                    if (mediaG > 255) { mediaG = 255; }
                    if (mediaR > 255) { mediaR = 255; }
                    if (mediaB < 0) { mediaB = 0; }
                    if (mediaG < 0) { mediaG = 0; }
                    if (mediaR < 0) { mediaR = 0; }

                    dataPtr[0] = (byte)(Math.Round(mediaB));
                    dataPtr[1] = (byte)(Math.Round(mediaG));
                    dataPtr[2] = (byte)(Math.Round(mediaR));

                    sum1B = sumB;
                    sum1G = sumG;
                    sum1R = sumR;

                    // ------------------------------------------|||---------------------------------------
                    //PROCESSAR O INTERIOR S/ MARGENS

                    dataPtr += 2*nChan + widthStep; //pointer da imagem no ponto (2,1)

                    for (ydest = 1; ydest < height - 1; ydest++)
                    {

                        for (xdest = 2; xdest < width - 1; xdest++)
                        {
                            dataPtrAux = (xdest - 2) * nChan + (ydest - 1) * widthStep + dataPtrOrigem;
                            sumB -= dataPtrAux[0];
                            sumG -= dataPtrAux[1];
                            sumR -= dataPtrAux[2];
                            dataPtrAux = (xdest - 2) * nChan + (ydest) * widthStep + dataPtrOrigem;
                            sumB -= dataPtrAux[0];
                            sumG -= dataPtrAux[1];
                            sumR -= dataPtrAux[2];
                            dataPtrAux = (xdest - 2) * nChan + (ydest + 1) * widthStep + dataPtrOrigem;
                            sumB -= dataPtrAux[0];
                            sumG -= dataPtrAux[1];
                            sumR -= dataPtrAux[2];
                            dataPtrAux = (xdest + 1) * nChan + (ydest - 1) * widthStep + dataPtrOrigem;
                            sumB += dataPtrAux[0];
                            sumG += dataPtrAux[1];
                            sumR += dataPtrAux[2];
                            dataPtrAux = (xdest + 1) * nChan + (ydest) * widthStep + dataPtrOrigem;
                            sumB += dataPtrAux[0];
                            sumG += dataPtrAux[1];
                            sumR += dataPtrAux[2];
                            dataPtrAux = (xdest + 1) * nChan + (ydest + 1) * widthStep + dataPtrOrigem;
                            sumB += dataPtrAux[0];
                            sumG += dataPtrAux[1];
                            sumR += dataPtrAux[2];
                            
                            mediaB = Math.Round(sumB / 9.0);
                            mediaG = Math.Round(sumG / 9.0);
                            mediaR = Math.Round(sumR / 9.0);

                            if (mediaB > 255) { mediaB = 255; }
                            if (mediaG > 255) { mediaG = 255; }
                            if (mediaR > 255) { mediaR = 255; }
                            if (mediaB <0) { mediaB = 0; }
                            if (mediaG <0) { mediaG = 0; }
                            if (mediaR <0) { mediaR = 0; }
                            dataPtr[0] = (byte)mediaB;
                            dataPtr[1] = (byte)mediaG;
                            dataPtr[2] = (byte)mediaR;
                            dataPtr += nChan; //termina no pixel (width-1)
                            
                            
                        }

                        dataPtr += 2 * nChan + padding; //avança para a próxima linha. Termina no pixel (1,height-1) CHECK

                        //sumB = sum1B;
                        //sumG = sum1G;
                        //sumR = sum1R;

                        dataPtrAux = (ydest - 1) * widthStep + dataPtrOrigem; //(0 , ydest-1)
                        sum1B -= dataPtrAux[0];
                        sum1G -= dataPtrAux[1];
                        sum1R -= dataPtrAux[2];
                        dataPtrAux = nChan + (ydest - 1) * widthStep + dataPtrOrigem; //(1 , ydest-1)
                        sum1B -= dataPtrAux[0];
                        sum1G -= dataPtrAux[1];
                        sum1R -= dataPtrAux[2];
                        dataPtrAux = 2 * nChan + (ydest - 1) * widthStep + dataPtrOrigem; //(2 , ydest-1)
                        sum1B -= dataPtrAux[0];
                        sum1G -= dataPtrAux[1];
                        sum1R -= dataPtrAux[2];
                        dataPtrAux = (ydest + 2) * widthStep + dataPtrOrigem; //(0 , ydest+1)
                        sum1B += dataPtrAux[0];
                        sum1G += dataPtrAux[1];
                        sum1R += dataPtrAux[2];
                        dataPtrAux = nChan + (ydest + 2) * widthStep + dataPtrOrigem; // (1 , ydest+1)
                        sum1B += dataPtrAux[0];
                        sum1G += dataPtrAux[1];
                        sum1R += dataPtrAux[2];
                        dataPtrAux = 2 * nChan + (ydest + 2) * widthStep + dataPtrOrigem; // (2 , ydest+1)
                        sum1B += dataPtrAux[0];
                        sum1G += dataPtrAux[1];
                        sum1R += dataPtrAux[2];

                        mediaB = Math.Round(sum1B / 9.0);
                        mediaG = Math.Round(sum1G / 9.0);
                        mediaR = Math.Round(sum1R / 9.0);

                        if (mediaB > 255) { mediaB = 255; }
                        if (mediaG > 255) { mediaG = 255; }
                        if (mediaR > 255) { mediaR = 255; }
                        if (mediaB < 0) { mediaB = 0; }
                        if (mediaG < 0) { mediaG = 0; }
                        if (mediaR < 0) { mediaR = 0; }

                        dataPtr[0] = (byte)mediaB;
                        dataPtr[1] = (byte)mediaG;
                        dataPtr[2] = (byte)mediaR;
                       
                        dataPtr += nChan;
                    }
                }
            }
        }

        public static void Mean_solutionC(Image<Bgr, byte> img, Image<Bgr, byte> imgUndo, int size)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgUndo.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();

                //int N = 3;
                int xdest = 0;
                int ydest = 0;
                //int xorigem, yorigem;
                uint sumB = 0;
                uint sumG = 0;
                uint sumR = 0;
                uint sum1B = 0;
                uint sum1G = 0;
                uint sum1R = 0;

                double sumCB = 0;
                double sumCG = 0;
                double sumCR = 0;

                double sumM1B = 0;
                double sumM1G = 0;
                double sumM1R = 0;
                double sumM2B = 0;     //soma das componentes nas margens
                double sumM2G = 0;
                double sumM2R = 0;
                double sumM3B = 0;
                double sumM3G = 0;
                double sumM3R = 0;
                double sumM4B = 0;
                double sumM4G = 0;
                double sumM4R = 0;

                double mediaB = 0;
                double mediaG = 0;
                double mediaR = 0;
                double counterSum = 0;

                if (nChan == 3)
                {
                    // ------------------------------------------|||---------------------------------------
                    //CALCULAR A MÉDIA PARA O 1º PIXEL DO INTERIOR

                    dataPtrAux = dataPtrOrigem;     //Pointer para calculo da média no pixel (0,0)

                    sumB = dataPtrAux[0];
                    sumG = dataPtrAux[1];
                    sumR = dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (1,0)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (2,0)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    dataPtrAux += widthStep - 2 * nChan;     //Pointer no pixel (0,1)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (1,1)

                    sumCB += dataPtrAux[0];
                    sumCG += dataPtrAux[1];
                    sumCR += dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (2,1)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    dataPtrAux += widthStep - 2 * nChan;     //Pointer no pixel (0,2)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (1,2)

                    sumCB += dataPtrAux[0];
                    sumCG += dataPtrAux[1];
                    sumCR += dataPtrAux[2];

                    dataPtrAux += nChan;     //Pointer no pixel (2,2)

                    sumB += dataPtrAux[0];
                    sumG += dataPtrAux[1];
                    sumR += dataPtrAux[2];

                    mediaB = sumB / 9.0;
                    mediaG = sumG / 9.0;
                    mediaR = sumR / 9.0;

                    if (mediaB > 255) { mediaB = 255; }
                    if (mediaG > 255) { mediaG = 255; }
                    if (mediaR > 255) { mediaR = 255; }
                    if (mediaB < 0) { mediaB = 0; }
                    if (mediaG < 0) { mediaG = 0; }
                    if (mediaR < 0) { mediaR = 0; }

                    dataPtr[0] = (byte)(Math.Round(mediaB));
                    dataPtr[1] = (byte)(Math.Round(mediaG));
                    dataPtr[2] = (byte)(Math.Round(mediaR));

                    sum1B = sumB;
                    sum1G = sumG;
                    sum1R = sumR;

                    // ------------------------------------------|||---------------------------------------
                    //PROCESSAR O INTERIOR S/ MARGENS

                    dataPtr += 2 * nChan + widthStep; //pointer da imagem no ponto (2,1)

                    xdest = 2;
                    
                        dataPtrAux = (xdest - 2) * nChan + dataPtrOrigem;
                        sumB -= dataPtrAux[0];
                        sumG -= dataPtrAux[1];
                        sumR -= dataPtrAux[2];
                        dataPtrAux = (xdest - 2) * nChan + widthStep + dataPtrOrigem;
                        sumB -= dataPtrAux[0];
                        sumG -= dataPtrAux[1];
                        sumR -= dataPtrAux[2];
                        dataPtrAux = (xdest - 2) * nChan + 2 * widthStep + dataPtrOrigem;
                        sumB -= dataPtrAux[0];
                        sumG -= dataPtrAux[1];
                        sumR -= dataPtrAux[2];
                        dataPtrAux = (xdest + 1) * nChan + dataPtrOrigem;
                        sumB += dataPtrAux[0];
                        sumG += dataPtrAux[1];
                        sumR += dataPtrAux[2];
                        dataPtrAux = (xdest + 1) * nChan + widthStep + dataPtrOrigem;
                        sumB += dataPtrAux[0];
                        sumG += dataPtrAux[1];
                        sumR += dataPtrAux[2];
                        dataPtrAux = (xdest + 1) * nChan + 2 * widthStep + dataPtrOrigem;
                        sumB += dataPtrAux[0];
                        sumG += dataPtrAux[1];
                        sumR += dataPtrAux[2];

                        mediaB = Math.Round(sumB / 9.0);
                        mediaG = Math.Round(sumG / 9.0);
                        mediaR = Math.Round(sumR / 9.0);

                        if (mediaB > 255) { mediaB = 255; }
                        if (mediaG > 255) { mediaG = 255; }
                        if (mediaR > 255) { mediaR = 255; }
                        if (mediaB < 0) { mediaB = 0; }
                        if (mediaG < 0) { mediaG = 0; }
                        if (mediaR < 0) { mediaR = 0; }

                        dataPtr[0] = (byte)mediaB;
                        dataPtr[1] = (byte)mediaG;
                        dataPtr[2] = (byte)mediaR;

                        dataPtr += nChan; //termina no pixel (width-1)


                    

                    for (ydest = 1; ydest < height - 1; ydest++)
                    {

                       

                        dataPtr += 2 * nChan + padding; //avança para a próxima linha. Termina no pixel (1,height-1) CHECK

                        //sumB = sum1B;
                        //sumG = sum1G;
                        //sumR = sum1R;

                        dataPtrAux = (ydest - 1) * widthStep + dataPtrOrigem; //(0 , ydest-1)
                        sum1B -= dataPtrAux[0];
                        sum1G -= dataPtrAux[1];
                        sum1R -= dataPtrAux[2];
                        dataPtrAux = nChan + (ydest - 1) * widthStep + dataPtrOrigem; //(1 , ydest-1)
                        sum1B -= dataPtrAux[0];
                        sum1G -= dataPtrAux[1];
                        sum1R -= dataPtrAux[2];
                        dataPtrAux = 2 * nChan + (ydest - 1) * widthStep + dataPtrOrigem; //(2 , ydest-1)
                        sum1B -= dataPtrAux[0];
                        sum1G -= dataPtrAux[1];
                        sum1R -= dataPtrAux[2];
                        dataPtrAux = (ydest + 2) * widthStep + dataPtrOrigem; //(0 , ydest+1)
                        sum1B += dataPtrAux[0];
                        sum1G += dataPtrAux[1];
                        sum1R += dataPtrAux[2];
                        dataPtrAux = nChan + (ydest + 2) * widthStep + dataPtrOrigem; // (1 , ydest+1)
                        sum1B += dataPtrAux[0];
                        sum1G += dataPtrAux[1];
                        sum1R += dataPtrAux[2];
                        dataPtrAux = 2 * nChan + (ydest + 2) * widthStep + dataPtrOrigem; // (2 , ydest+1)
                        sum1B += dataPtrAux[0];
                        sum1G += dataPtrAux[1];
                        sum1R += dataPtrAux[2];

                        mediaB = Math.Round(sum1B / 9.0);
                        mediaG = Math.Round(sum1G / 9.0);
                        mediaR = Math.Round(sum1R / 9.0);

                        if (mediaB > 255) { mediaB = 255; }
                        if (mediaG > 255) { mediaG = 255; }
                        if (mediaR > 255) { mediaR = 255; }
                        if (mediaB < 0) { mediaB = 0; }
                        if (mediaG < 0) { mediaG = 0; }
                        if (mediaR < 0) { mediaR = 0; }

                        dataPtr[0] = (byte)mediaB;
                        dataPtr[1] = (byte)mediaG;
                        dataPtr[2] = (byte)mediaR;

                        dataPtr += nChan;
                    }
                }
            }
        }

        public static void Scale(Image<Bgr, byte> img, Image<Bgr, byte> imgUndo, float scaleFactor)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgUndo.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();


                int xorigem, yorigem, xdest, ydest;

                if (nChan == 3)
                {
                    for (ydest = 0; ydest < height; ydest++)
                    {
                        for (xdest = 0; xdest < width; xdest++)
                        {
                            xorigem = (int)(xdest / scaleFactor);
                            yorigem = (int)(ydest / scaleFactor);


                            if (xorigem >= 0 && yorigem >= 0 && xorigem < width && yorigem < height)
                            {
                                dataPtrAux = (yorigem * widthStep + xorigem * nChan + dataPtrOrigem);
                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];
                            }
                            else
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }


                            dataPtr += nChan;

                        }

                        dataPtr += padding;
                    }

                }
            }
        }

        public static void Scale_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgUndo, float scaleFactor)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgUndo.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();


                double xorigem, yorigem, xdest, ydest, dx, dy, b1B, b1G, b1R, b2B, b2G, b2R;
                int j, k, jkB, jkplus1B, jplus1kB, jplus1kplus1B,
                    jkG, jkplus1G, jplus1kG, jplus1kplus1G,
                    jkR, jkplus1R, jplus1kR, jplus1kplus1R, btotalB, btotalG, btotalR;

                if (nChan == 3)
                {
                    for (ydest = 0; ydest < height; ydest++)
                    {
                        for (xdest = 0; xdest < width; xdest++)
                        {
                            xorigem = (xdest /scaleFactor);
                            yorigem = (ydest / scaleFactor);

                            if (xorigem >= 0 && yorigem >= 0 && xorigem < width && yorigem < height)
                            { 

                                j = (int)Math.Floor(yorigem);
                                k = (int)Math.Floor(xorigem);

                                dx = xorigem - j; //desvio que o pixel tem para o inteiro anterior
                                dy = yorigem - k;

                                if (dx > 0 || dy > 0)
                                {
                                    dataPtrAux = (j * widthStep + k * nChan + dataPtrOrigem);
                                    jkB = dataPtrAux[0];
                                    jkG = dataPtrAux[1];
                                    jkR = dataPtrAux[2];
                                    dataPtrAux = ((j + 1) * widthStep + k * nChan + dataPtrOrigem);
                                    jplus1kB = dataPtrAux[0];
                                    jplus1kG = dataPtrAux[1];
                                    jplus1kR = dataPtrAux[2];
                                    dataPtrAux = (j * widthStep + (k + 1) * nChan + dataPtrOrigem);
                                    jkplus1B = dataPtrAux[0];
                                    jkplus1G = dataPtrAux[1];
                                    jkplus1R = dataPtrAux[2];
                                    dataPtrAux = ((j + 1) * widthStep + (k + 1) * nChan + dataPtrOrigem);
                                    jplus1kplus1B = dataPtrAux[0];
                                    jplus1kplus1G = dataPtrAux[1];
                                    jplus1kplus1R = dataPtrAux[2];

                                    //b1B = Math.Round(jkB+(jkplus1B - jkB)*dx); //Same?

                                    b1B = Math.Round((1 - dx) * jkB + dx * jkplus1B);
                                    b2B = Math.Round((1 - dx) * jplus1kB + dx * jplus1kplus1B);
                                    btotalB = (int)Math.Round((1 - dy) * b1B + dy * b2B);
                                    b1G = Math.Round((1 - dx) * jkG + dx * jkplus1G);
                                    b2G = Math.Round((1 - dx) * jplus1kG + dx * jplus1kplus1G);
                                    btotalG = (int)Math.Round((1 - dy) * b1G + dy * b2G);
                                    b1R = Math.Round((1 - dx) * jkR + dx * jkplus1R);
                                    b2R = Math.Round((1 - dx) * jplus1kR + dx * jplus1kplus1R);
                                    btotalR = (int)Math.Round((1 - dy) * b1R + dy * b2R);

                                    if (btotalB < 0) { btotalB = 0; }
                                    if (btotalG < 0) { btotalG = 0; }
                                    if (btotalR < 0) { btotalR = 0; }
                                    if (btotalB > 255) { btotalB = 255; }
                                    if (btotalG > 255) { btotalG = 255; }
                                    if (btotalR > 255) { btotalR = 255; }
                                    dataPtr[0] = (byte)btotalB;
                                    dataPtr[1] = (byte)btotalG;
                                    dataPtr[2] = (byte)btotalR;

                                }
                            }
                            else
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }


                            dataPtr += nChan;

                        }

                        dataPtr += padding;
                    }

                }
            }
        }

        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgUndo, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgUndo.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();


                int xorigem, yorigem, xdest, ydest;

                if (nChan == 3)
                {
                    for (ydest = 0; ydest < height; ydest++)
                    {
                        for (xdest = 0; xdest < width; xdest++)
                        {
                            xorigem = (int)Math.Round(((xdest - (width / 2)) / scaleFactor) + 250);
                            yorigem = (int)Math.Round(((ydest - (height / 2)) / scaleFactor) + 310);


                            if (xorigem >= 0 && yorigem >= 0 && xorigem < width && yorigem < height)
                            {
                                dataPtrAux = (yorigem * widthStep + xorigem * nChan + dataPtrOrigem);
                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];
                            }
                            else
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }


                            dataPtr += nChan;

                        }

                        dataPtr += padding;
                    }

                }
            }
        }

        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // iguala todas as componentes à vermelha
                            dataPtr[0] =  dataPtr[2];
                            dataPtr[1] = dataPtr[2];

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void BrightContrast(Image<Bgr, byte> img, int brightness, double contrast)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                byte newB, newG, newR;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                                newB = (byte)(Math.Round(dataPtr[0] * contrast + brightness));
                                newG = (byte)(Math.Round(dataPtr[1] * contrast + brightness));
                                newR = (byte)(Math.Round(dataPtr[2] * contrast + brightness));

                            if (newB < 0) { newB = 0; }
                            if (newG < 0) { newG = 0; }
                            if (newR < 0) { newR = 0; }
                            if (newB > 255) { newB = 255; }
                            if (newG > 255) { newG = 255; }
                            if (newR > 255) { newR = 255; }
 
                            dataPtr[0] = newB;
                            dataPtr[1] = newG;
                            dataPtr[2] = newR;
                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
         }

        public static void ConvertToBW_Otsu(Image<Bgr, byte> img)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int[] hist = new int[256];
                double[] probabilities = new double[hist.Length];
                double[] variance = new double[hist.Length];

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int widthStep = m.widthStep;
                int x, y;
                
                int sum = 0;
                int level = 0;
                double max = 0;
                double var = 0;

                double q1 = 0;
                double q2 = 0;
                
                double u1 = 0;
                double u2 = 0;

                //double[] miu1 = new double[histogram.Length];
                //double[] miu2 = new double[histogram.Length];

                //double[] variance1 = new double[histogram.Length];
                //double[] variance2 = new double[histogram.Length];

                //double[] totalVari = new double[histogram.Length];
                //int threshold = 0;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            //calcula o nível de cinzento
                            int gray = (int)Math.Round((blue + green + red) / 3.0);

                            hist[gray]++;

                            sum++;
                            //advance the pointer to the next pixel
                            dataPtr += nChan;
                            }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }

                    dataPtr -= ((width - 1) * nChan + (height - 1) * widthStep);
                    
                   
                    u2 = 0;
                    u1 = 0;

                    for (int i = 2; i < hist.Length; i++)
                    {
                        probabilities[i] = hist[i] / (double)sum;
                        u2 = u2 + i * probabilities[i];

                        Debug.WriteLine(probabilities[i] + " " + hist[i]);

                    }
                    q1 = probabilities[0] + probabilities[1];
                    q2 = 1 - q1;
                    
                    if(q1 > 0)
                    {
                        u1 = probabilities[1] / q1;
                    }
                    

                    level = 1;

                    if (u1 > 0)
                    {
                        max = (q1 * q2 * ((u1 / q1 - u2 / q2) * (u1 / q1 - u2 / q2)));
                    }
                    else
                    {
                        max = 0;
                    }

                    for (int i = 2; i < hist.Length; i++)
                    {
                        q1 = q1 + probabilities[i];

                        if (q1 == 0)
                        {
                            continue; // nothing on the left side -skip
                        }
                        q2 = q2 - probabilities[i];

                        if (q2 == 0)
                        { 
                            break; // nothing on the right side -finish
                        }

                        u1 = u1 + i * probabilities[i];
                        u2 = u2 - i * probabilities[i];

                        var = q1 * q2 * (((u1 / q1) - (u2 / q2)) * ((u1 / q1) - (u2 / q2)));

                        if (var > max)
                        {
                            level = i;
                            max = var;
                        }
                    }
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            //calcula o nível de cinzento
                            int gray = (int)Math.Round((blue + green + red) / 3.0);

                            if (gray > level)
                            {
                                dataPtr[0] = 255; //PIXEL BRANCO
                                dataPtr[1] = 255;
                                dataPtr[2] = 255;
                            }
                            else
                            {
                                dataPtr[0] = 0; //PIXEL PRETO
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;
                            }


                            // avança o pointer para o pixel anterior
                            dataPtr += nChan;

                        }

                        //no início da linha salta o padding para o fim da linha acima
                        dataPtr += padding;
                    }


                    //for (y = 0; y < height; y++)
                    //{
                    //    for (x = 0; x < width; x++)
                    //    {
                    //        //obtém as 3 componentes
                    //        blue = dataPtr[0];
                    //        green = dataPtr[1];
                    //        red = dataPtr[2];

                    //        //calcula o nível de cinzento
                    //        int gray = (int)Math.Round((blue + green + red) / 3.0);

                    //        histogram[gray]++;

                            //if (gray > i)
                            //{
                            //    dataPtr[0] = 255; //PIXEL BRANCO
                            //    dataPtr[1] = 255;
                            //    dataPtr[2] = 255;
                            //}
                            //else
                            //{
                            //    dataPtr[0] = 0; //PIXEL PRETO
                            //    dataPtr[1] = 0;
                            //    dataPtr[2] = 0;
                            //}


                            // advance the pointer to the next pixel
                    //        dataPtr += nChan;
                    //        sum++;
                    //    }

                    //    //at the end of the line advance the pointer by the aligment bytes (padding)
                    //    dataPtr += padding;
                    //}

                    //        //calcula a probabilidade para cada nivel de cinzento
                    //        for (int i = 0; i < histogram.Length; i++)
                    //        {
                    //            probabilities[i] = (histogram[i]/(double)sum);
                    //        }
                    //        sum = 0;
                    //        //calcular q1/2, miu1/2, variance1/2 e totalVari para cada threshold t
                    //        for (int t = 0; t < histogram.Length; t++)
                    //        {
                    //            //somas as probabilidades de 0 até t
                    //            for (int i = 0; i <= t; i++)
                    //            {
                    //                q1[t] += probabilities[i];
                    //            }
                    //            //somas as probabilidades de t+1 até 255
                    //            for (int i = 255; i >= t + 1; i--)
                    //            {
                    //                q2[t] += probabilities[i];
                    //            }
                    //            //calcula miu1(t)
                    //            for (int i = 0; i <= t; i++)
                    //            {
                    //                miu1[t] += (i * probabilities[i]) / q1[t];
                    //            }
                    //            //calcula miu2(t)
                    //            for (int i = 255; i >= t + 1; i--)
                    //            {
                    //                miu2[t] += (i * probabilities[i]) / q2[t];
                    //            }
                    //            for (int i = 0; i <= t; i++)
                    //            {
                    //                variance1[t] += (((miu1[t] - i) * (miu1[t] - i)) * probabilities[i]) / q1[t];
                    //            }
                    //            for (int i = 255; i >= t + 1; i--)
                    //            {
                    //                variance2[t] += (((miu2[t] - i) * (miu2[t] - i)) * probabilities[i]) / q2[t];
                    //            }
                    //            totalVari[t] = q1[t] * variance1[t] + q2[t] * variance2[t];



                    //        }
                    //        threshold = Array.IndexOf(totalVari, totalVari.Min());

                    //        dataPtr -= ((height - 1)*widthStep + (width-1)*nChan);

                    //        
                }
            }
        }

        public static void ConvertToBW(Emgu.CV.Image<Bgr, byte> img, int threshold)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;
                
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                int i = threshold;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            //calcula o nível de cinzento
                            int gray = (int)Math.Round((blue + green + red) / 3.0);

                            if(gray > i)
                            {
                                dataPtr[0] = 255; //PIXEL BRANCO
                                dataPtr[1] = 255;
                                dataPtr[2] = 255;
                            }
                            else
                            {
                                dataPtr[0] = 0; //PIXEL PRETO
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;
                            }
                          

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgUndo)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgUndo.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();

                int xdest = 0;
                int ydest = 0;
                int xfiltro = 0;
                int yfiltro = 0;

                int indiceFiltro = 0;
                
                double sumB = 0;
                double sumG = 0;
                double sumR = 0;

                double sumCB = 0;
                double sumCG = 0;
                double sumCR = 0;

                double sumMB = 0;
                double sumMG = 0;
                double sumMR = 0; //soma das componentes nas margens

                float[] valorPixFiltroG = new float[9];
                float[] valorPixFiltroB = new float[9];
                float[] valorPixFiltroR = new float[9];
                
                double[] sumDiffB = new double[9];
                double[] sumDiffG = new double[9];
                double[] sumDiffR = new double[9];
                double[] sumDiffTotal = new double[9];

                double valorMin = 0;
                int indexMin = 0;

                if (nChan == 3)
                {
                    // ------------------------------------------|||---------------------------------------
                    //PROCESSAR O INTERIOR S/ MARGENS

                    dataPtr += nChan + widthStep; //pointer da imagem no ponto (1,1)

                    //percorre cada linha de pixeis
                    for (ydest = 1; ydest < height - 1; ydest++)

                    {
                        //percorre cada pixel de cada linha
                        for (xdest = 1; xdest < width - 1; xdest++)

                        {
                            //percorre cada linha do filtro
                            for (yfiltro = ydest - 1; yfiltro < ydest + 2; yfiltro++)
                            {
                                //percorre cada pixel de cada linha do filtro
                                for (xfiltro = xdest - 1; xfiltro < xdest + 2; xfiltro++)
                                {
                                    dataPtrAux = xfiltro * nChan + yfiltro * widthStep + dataPtrOrigem;

                                    valorPixFiltroB[indiceFiltro] = dataPtrAux[0];
                                    valorPixFiltroG[indiceFiltro] = dataPtrAux[1];
                                    valorPixFiltroR[indiceFiltro] = dataPtrAux[2];

                                    indiceFiltro++;
                                }
                            }

                            indiceFiltro = 0;
                            int i = 0;


                            for (i = 0; i <= 8; i++)
                            {
                                sumDiffB[i] +=        //CALCULAR A SOMA DAS DIFERENÇAS DE CADA PIXEL
                                    Math.Abs(valorPixFiltroB[i] - valorPixFiltroB[0]) +
                                    Math.Abs(valorPixFiltroB[i] - valorPixFiltroB[1]) +
                                    Math.Abs(valorPixFiltroB[i] - valorPixFiltroB[2]) +
                                    Math.Abs(valorPixFiltroB[i] - valorPixFiltroB[3]) +
                                    Math.Abs(valorPixFiltroB[i] - valorPixFiltroB[4]) +
                                    Math.Abs(valorPixFiltroB[i] - valorPixFiltroB[5]) +
                                    Math.Abs(valorPixFiltroB[i] - valorPixFiltroB[6]) +
                                    Math.Abs(valorPixFiltroB[i] - valorPixFiltroB[7]) +
                                    Math.Abs(valorPixFiltroB[i] - valorPixFiltroB[8]);
                                sumDiffG[i] +=
                                    Math.Abs(valorPixFiltroG[i] - valorPixFiltroG[0]) +
                                    Math.Abs(valorPixFiltroG[i] - valorPixFiltroG[1]) +
                                    Math.Abs(valorPixFiltroG[i] - valorPixFiltroG[2]) +
                                    Math.Abs(valorPixFiltroG[i] - valorPixFiltroG[3]) +
                                    Math.Abs(valorPixFiltroG[i] - valorPixFiltroG[4]) +
                                    Math.Abs(valorPixFiltroG[i] - valorPixFiltroG[5]) +
                                    Math.Abs(valorPixFiltroG[i] - valorPixFiltroG[6]) +
                                    Math.Abs(valorPixFiltroG[i] - valorPixFiltroG[7]) +
                                    Math.Abs(valorPixFiltroG[i] - valorPixFiltroG[8]);
                                sumDiffR[i] +=
                                    Math.Abs(valorPixFiltroR[i] - valorPixFiltroR[0]) +
                                     Math.Abs(valorPixFiltroR[i] - valorPixFiltroR[1]) +
                                     Math.Abs(valorPixFiltroR[i] - valorPixFiltroR[2]) +
                                     Math.Abs(valorPixFiltroR[i] - valorPixFiltroR[3]) +
                                     Math.Abs(valorPixFiltroR[i] - valorPixFiltroR[4]) +
                                     Math.Abs(valorPixFiltroR[i] - valorPixFiltroR[5]) +
                                     Math.Abs(valorPixFiltroR[i] - valorPixFiltroR[6]) +
                                     Math.Abs(valorPixFiltroR[i] - valorPixFiltroR[7]) +
                                     Math.Abs(valorPixFiltroR[i] - valorPixFiltroR[8]);

                                sumDiffTotal[i] = sumDiffB[i] + sumDiffG[i] + sumDiffR[i];
                                valorMin = sumDiffTotal.Min();
                                indexMin = sumDiffTotal.ToList().IndexOf(valorMin);
                            }

                            dataPtr[0] = (byte)valorPixFiltroB[indexMin];
                            dataPtr[1] = (byte)valorPixFiltroG[indexMin];
                            dataPtr[2] = (byte)valorPixFiltroR[indexMin];
                           
                            dataPtr += nChan;
                        }
                        dataPtr += 2 * nChan + padding;
                    }
                //    // ------------------------------------------|||---------------------------------------
                //    // PROCESSAR OS CANTOS
                //    // 1º CANTO (0,0)

                //    dataPtr -= (nChan + (height - 1) * widthStep);
                //    //Pointer na imagem de origem no pixel (0,0) CHECK

                //    dataPtrAux = dataPtrOrigem;
                //    //Pointer Auxiliar no pixel (0,0)

                //    sumCB = (b1 + b2 + b4 + b5) * dataPtrAux[0];
                //    sumCG = (b1 + b2 + b4 + b5) * dataPtrAux[1];
                //    sumCR = (b1 + b2 + b4 + b5) * dataPtrAux[2];

                //    dataPtrAux += nChan;
                //    //Pointer Auxiliar no pixel (1,0)

                //    sumCB += (b3 + b6) * dataPtrAux[0];
                //    sumCG += (b3 + b6) * dataPtrAux[1];
                //    sumCR += (b3 + b6) * dataPtrAux[2];

                //    dataPtrAux += widthStep - nChan;
                //    //Pointer Auxiliar no pixel (0,1)

                //    sumCB += (b7 + b8) * dataPtrAux[0];
                //    sumCG += (b7 + b8) * dataPtrAux[1];
                //    sumCR += (b7 + b8) * dataPtrAux[2];

                //    dataPtrAux += nChan;
                //    //Pointer Auxiliar no pixel (1,1)

                //    sumCB += b9 + dataPtrAux[0];
                //    sumCG += b9 + dataPtrAux[1];
                //    sumCR += b9 + dataPtrAux[2];

                //    dataPtr[0] = (byte)(Math.Round(sumCB / 9));
                //    dataPtr[1] = (byte)(Math.Round(sumCG / 9));
                //    dataPtr[2] = (byte)(Math.Round(sumCR / 9));


                //    // ------------------------------------------|||---------------------------------------
                //    // 2º CANTO (width-1,0)

                //    dataPtr += (width - 1) * nChan;
                //    //Pointer na imagem principal no pixel (width-1,0)
                //    dataPtrAux = (width - 2) * nChan + dataPtrOrigem;
                //    //Pointer Auxiliar no pixel (width-2, 0)

                //    sumCB = (b1 + b4) * dataPtrAux[0];
                //    sumCG = (b1 + b4) * dataPtrAux[1];
                //    sumCR = (b1 + b4) * dataPtrAux[2];

                //    dataPtrAux += nChan;
                //    //Pointer Auxiliar no pixel (width-1,0)

                //    sumCB += (b2 + b3 + b5 + b6) * dataPtrAux[0];
                //    sumCG += (b2 + b3 + b5 + b6) * dataPtrAux[1];
                //    sumCR += (b2 + b3 + b5 + b6) * dataPtrAux[2];

                //    dataPtrAux += widthStep - nChan;
                //    //Pointer Auxiliar no pixel (width-2,1)

                //    sumCB += b7 * dataPtrAux[0];
                //    sumCG += b7 * dataPtrAux[1];
                //    sumCR += b7 * dataPtrAux[2];

                //    dataPtrAux += nChan;
                //    //Pointer Auxiliar no pixel (width-1,1)

                //    sumCB += (b8 + b9) * dataPtrAux[0];
                //    sumCG += (b8 + b9) * dataPtrAux[1];
                //    sumCR += (b8 + b9) * dataPtrAux[2];

                //    dataPtr[0] = (byte)(Math.Round(sumCB / 9));
                //    dataPtr[1] = (byte)(Math.Round(sumCG / 9));
                //    dataPtr[2] = (byte)(Math.Round(sumCR / 9));

                //    // ------------------------------------------|||---------------------------------------
                //    // 3º CANTO (width-1,height-1)

                //    dataPtr += (height - 1) * widthStep;
                //    //Pointer na imagem principal no pixel (width-1,height-1)
                //    dataPtrAux = ((height - 2) * widthStep) + ((width - 2) * nChan) + dataPtrOrigem;
                //    //Pointer Auxiliar no pixel (width-2, height-2)

                //    sumCB = b1 * dataPtrAux[0];
                //    sumCG = b1 * dataPtrAux[1];
                //    sumCR = b1 * dataPtrAux[2];

                //    dataPtrAux += nChan;
                //    //Pointer Auxiliar no pixel (width-1,height-2)

                //    sumCB += (b2 + b3) * dataPtrAux[0];
                //    sumCG += (b2 + b3) * dataPtrAux[1];
                //    sumCR += (b2 + b3) * dataPtrAux[2];

                //    dataPtrAux += widthStep - nChan;
                //    //Pointer Auxiliar no pixel (width-2,height-1)

                //    sumCB += (b4 + b7) * dataPtrAux[0];
                //    sumCG += (b4 + b7) * dataPtrAux[1];
                //    sumCR += (b4 + b7) * dataPtrAux[2];

                //    dataPtrAux += nChan;
                //    //Pointer Auxiliar no pixel (width-1,height-1)

                //    sumCB += (b5 + b6 + b8 + b9) * dataPtrAux[0];
                //    sumCG += (b5 + b6 + b8 + b9) * dataPtrAux[1];
                //    sumCR += (b5 + b6 + b8 + b9) * dataPtrAux[2];

                //    dataPtr[0] = (byte)(Math.Round(sumCB / 9));
                //    dataPtr[1] = (byte)(Math.Round(sumCG / 9));
                //    dataPtr[2] = (byte)(Math.Round(sumCR / 9));


                //    // ------------------------------------------|||---------------------------------------
                //    // 4º CANTO (0,height-1)

                //    dataPtr -= (width - 1) * nChan;
                //    //Pointer na imagem principal no pixel (0, height-1)
                //    dataPtrAux = (height - 2) * widthStep + dataPtrOrigem;
                //    //Pointer Auxiliar no pixel (0, height-2)

                //    sumCB = (b1 + b2) * dataPtrAux[0];
                //    sumCG = (b1 + b2) * dataPtrAux[1];
                //    sumCR = (b1 + b2) * dataPtrAux[2];

                //    dataPtrAux += nChan;
                //    //Pointer Auxiliar no pixel (1, height-2)

                //    sumCB += b3 * dataPtrAux[0];
                //    sumCG += b3 * dataPtrAux[1];
                //    sumCR += b3 * dataPtrAux[2];

                //    dataPtrAux += widthStep - nChan;
                //    //Pointer Auxiliar no pixel (0, height-1)

                //    sumCB += (b4 + b5 + b7 + b8) * dataPtrAux[0];
                //    sumCG += (b4 + b5 + b7 + b8) * dataPtrAux[1];
                //    sumCR += (b4 + b5 + b7 + b8) * dataPtrAux[2];

                //    dataPtrAux += nChan;
                //    //Pointer Auxiliar no pixel (1, height-1)

                //    sumCB += (b6 + b9) * dataPtrAux[0];
                //    sumCG += (b6 + b9) * dataPtrAux[1];
                //    sumCR += (b6 + b9) * dataPtrAux[2];

                //    dataPtr[0] = (byte)(Math.Round(sumCB / 9));
                //    dataPtr[1] = (byte)(Math.Round(sumCG / 9));
                //    dataPtr[2] = (byte)(Math.Round(sumCR / 9));

                //    //Pointer na imagem principal no pixel (0, height-1)

                //    // ------------------------------------------|||---------------------------------------
                //    // FINALMENTE PROCESSAMOS AS MARGENS S/ CANTOS
                //    // MARGEM SUPERIOR (M1)

                //    dataPtr -= ((height - 1) * widthStep) - nChan;
                //    //Pointer na imagem principal no pixel (1,0)

                //    //Pointer Auxiliar ao longo da linha y=0
                //    for (xdest = 1; xdest < width - 1; xdest++)

                //    {
                //        dataPtrAux = ((xdest - 1) * nChan + dataPtrOrigem);
                //        //pixel anterior ao processado

                //        sumMB = (b1 + b4) * dataPtrAux[0];
                //        sumMG = (b1 + b4) * dataPtrAux[1];
                //        sumMR = (b1 + b4) * dataPtrAux[2];

                //        dataPtrAux = ((xdest) * nChan + dataPtrOrigem);

                //        sumMB += (b5 + b2) * dataPtrAux[0];
                //        sumMG += (b5 + b2) * dataPtrAux[1];
                //        sumMR += (b5 + b2) * dataPtrAux[2];

                //        dataPtrAux = ((xdest + 1) * nChan + dataPtrOrigem);
                //        //pixel posterior ao processado

                //        sumMB += (b3 + b6) * dataPtrAux[0];
                //        sumMG += (b3 + b6) * dataPtrAux[1];
                //        sumMR += (b3 + b6) * dataPtrAux[2];


                //        dataPtrAux = (widthStep + (xdest - 1) * nChan + dataPtrOrigem);
                //        //linha seguinte ao pixel a ser processado

                //        sumMB += b7 * dataPtrAux[0];
                //        sumMG += b7 * dataPtrAux[1];
                //        sumMR += b7 * dataPtrAux[2];

                //        dataPtrAux = (widthStep + (xdest) * nChan + dataPtrOrigem);

                //        sumMB += b8 * dataPtrAux[0];
                //        sumMG += b8 * dataPtrAux[1];
                //        sumMR += b8 * dataPtrAux[2];

                //        dataPtrAux = (widthStep + (xdest + 1) * nChan + dataPtrOrigem);

                //        sumMB += b9 * dataPtrAux[0];
                //        sumMG += b9 * dataPtrAux[1];
                //        sumMR += b9 * dataPtrAux[2];

                //        sumMB = Math.Round(sumMB / 9);
                //        sumMG = Math.Round(sumMG / 9);
                //        sumMR = Math.Round(sumMR / 9);

                //        dataPtr[0] = (byte)(sumMB);
                //        dataPtr[1] = (byte)(sumMG);
                //        dataPtr[2] = (byte)(sumMR);

                //        dataPtr += nChan; //termina quando dataPtr = (width-1 ,0) CHECK

                //    }

                //    // ------------------------------------------|||---------------------------------------
                //    // MARGEM LATERAL DIREITA (M2)

                //    dataPtr += widthStep;
                //    //Pointer na imagem principal no pixel (width-1,1)

                //    //Pointer ao longo da coluna x=width-1
                //    for (ydest = 1; ydest < height - 1; ydest++)

                //    {
                //        dataPtrAux = ((width - 1) * nChan + (ydest - 1) * widthStep + dataPtrOrigem);
                //        //ir ao pixel acima do pixel a ser processado

                //        sumMB = (b2 + b3) * dataPtrAux[0];
                //        sumMG = (b2 + b3) * dataPtrAux[1];
                //        sumMR = (b2 + b3) * dataPtrAux[2];

                //        dataPtrAux = ((width - 1) * nChan + (ydest) * widthStep + dataPtrOrigem);

                //        sumMB += (b5 + b6) * dataPtrAux[0];
                //        sumMG += (b5 + b6) * dataPtrAux[1];
                //        sumMR += (b5 + b6) * dataPtrAux[2];

                //        dataPtrAux = ((width - 1) * nChan + (ydest + 1) * widthStep + dataPtrOrigem);
                //        //ir ao pixel abaixo do pixel a ser processado

                //        sumMB += (b8 + b9) * dataPtrAux[0];
                //        sumMG += (b8 + b9) * dataPtrAux[1];
                //        sumMR += (b8 + b9) * dataPtrAux[2];


                //        dataPtrAux = ((width - 2) * nChan + ((ydest - 1) * widthStep) + dataPtrOrigem);
                //        //coluna anterior ao pixel a ser processado

                //        sumMB += b1 * dataPtrAux[0];
                //        sumMG += b1 * dataPtrAux[1];
                //        sumMR += b1 * dataPtrAux[2];

                //        dataPtrAux = ((width - 2) * nChan + (ydest * widthStep) + dataPtrOrigem);

                //        sumMB += b4 * dataPtrAux[0];
                //        sumMG += b4 * dataPtrAux[1];
                //        sumMR += b4 * dataPtrAux[2];

                //        dataPtrAux = ((width - 2) * nChan + ((ydest + 1) * widthStep) + dataPtrOrigem); //coluna anterior ao pixel a ser processado

                //        sumMB += b7 * dataPtrAux[0];
                //        sumMG += b7 * dataPtrAux[1];
                //        sumMR += b7 * dataPtrAux[2];

                //        sumMB = Math.Round(sumMB / 9);
                //        sumMG = Math.Round(sumMG / 9);
                //        sumMR = Math.Round(sumMR / 9);

                //        dataPtr[0] = (byte)(sumMB);
                //        dataPtr[1] = (byte)(sumMG);
                //        dataPtr[2] = (byte)(sumMR);

                //        dataPtr += widthStep;
                //        //temina quando dataPtr = (width-1, height-1)
                //    }

                //    // ------------------------------------------|||---------------------------------------
                //    // MARGEM INFERIOR (M3)

                //    dataPtr -= (width - 2) * nChan;
                //    //Pointer na imagem principal no pixel (1,height-1)

                //    //Pointer ao longo da linha y=height-1
                //    for (xdest = 1; xdest < width - 1; xdest++)

                //    {
                //        dataPtrAux = ((xdest - 1) * nChan + (height - 1) * widthStep + dataPtrOrigem);
                //        //pixel anterior ao processado

                //        sumMB = (b7 + b4) * dataPtrAux[0];
                //        sumMG = (b7 + b4) * dataPtrAux[1];
                //        sumMR = (b7 + b4) * dataPtrAux[2];

                //        dataPtrAux = ((xdest) * nChan + (height - 1) * widthStep + dataPtrOrigem);

                //        sumMB += (b5 + b8) * dataPtrAux[0];
                //        sumMG += (b5 + b8) * dataPtrAux[1];
                //        sumMR += (b5 + b8) * dataPtrAux[2];

                //        dataPtrAux = ((xdest + 1) * nChan + (height - 1) * widthStep + dataPtrOrigem);
                //        //pixel posterior ao processado

                //        sumMB += (b9 + b6) * dataPtrAux[0];
                //        sumMG += (b9 + b6) * dataPtrAux[1];
                //        sumMR += (b9 + b6) * dataPtrAux[2];


                //        dataPtrAux = ((xdest - 1) * nChan + (height - 2) * widthStep + dataPtrOrigem);
                //        //linha anterior ao pixel a ser processado

                //        sumMB += b1 * dataPtrAux[0];
                //        sumMG += b1 * dataPtrAux[1];
                //        sumMR += b1 * dataPtrAux[2];

                //        dataPtrAux = ((xdest) * nChan + (height - 2) * widthStep + dataPtrOrigem);

                //        sumMB += b2 * dataPtrAux[0];
                //        sumMG += b2 * dataPtrAux[1];
                //        sumMR += b2 * dataPtrAux[2];

                //        dataPtrAux = ((xdest + 1) * nChan + (height - 2) * widthStep + dataPtrOrigem);

                //        sumMB += b3 * dataPtrAux[0];
                //        sumMG += b3 * dataPtrAux[1];
                //        sumMR += b3 * dataPtrAux[2];

                //        sumMB = Math.Round(sumMB / 9);
                //        sumMG = Math.Round(sumMG / 9);
                //        sumMR = Math.Round(sumMR / 9);

                //        dataPtr[0] = (byte)(sumMB);
                //        dataPtr[1] = (byte)(sumMG);
                //        dataPtr[2] = (byte)(sumMR);

                //        dataPtr += nChan; //termina quando dataPtr = (width-1 ,height -1) CHECK

                //    }

                //    // ------------------------------------------|||---------------------------------------
                //    // MARGEM LATERAL ESQUERDA (M4)

                //    dataPtr -= ((width - 1) * nChan + (height - 2) * widthStep);
                //    //Pointer na imagem principal no pixel (0,1)

                //    //Pointer ao longo da coluna x=0
                //    for (ydest = 1; ydest < height - 1; ydest++)

                //    {
                //        dataPtrAux = (ydest - 1) * widthStep + dataPtrOrigem;
                //        //ir ao pixel acima do pixel a ser processado

                //        sumMB = (b2 + b1) * dataPtrAux[0];
                //        sumMG = (b2 + b1) * dataPtrAux[1];
                //        sumMR = (b2 + b1) * dataPtrAux[2];

                //        dataPtrAux = (ydest) * widthStep + dataPtrOrigem;

                //        sumMB += (b5 + b4) * dataPtrAux[0];
                //        sumMG += (b5 + b4) * dataPtrAux[1];
                //        sumMR += (b5 + b4) * dataPtrAux[2];

                //        dataPtrAux = (ydest + 1) * widthStep + dataPtrOrigem;
                //        //ir ao pixel abaixo do pixel a ser processado

                //        sumMB += (b8 + b7) * dataPtrAux[0];
                //        sumMG += (b8 + b7) * dataPtrAux[1];
                //        sumMR += (b8 + b7) * dataPtrAux[2];


                //        dataPtrAux = (width * nChan + ((ydest - 1) * widthStep) + dataPtrOrigem);
                //        //coluna seguinte ao pixel a ser processado

                //        sumMB += b3 * dataPtrAux[0];
                //        sumMG += b3 * dataPtrAux[1];
                //        sumMR += b3 * dataPtrAux[2];

                //        dataPtrAux = (width * nChan + (ydest * widthStep) + dataPtrOrigem);

                //        sumMB += b6 * dataPtrAux[0];
                //        sumMG += b6 * dataPtrAux[1];
                //        sumMR += b6 * dataPtrAux[2];

                //        dataPtrAux = (width * nChan + ((ydest + 1) * widthStep) + dataPtrOrigem); //coluna anterior ao pixel a ser processado

                //        sumMB += b9 * dataPtrAux[0];
                //        sumMG += b9 * dataPtrAux[1];
                //        sumMR += b9 * dataPtrAux[2];

                //        sumMB = Math.Round(sumMB / 9);
                //        sumMG = Math.Round(sumMG / 9);
                //        sumMR = Math.Round(sumMR / 9);

                //        dataPtr[0] = (byte)(sumMB);
                //        dataPtr[1] = (byte)(sumMG);
                //        dataPtr[2] = (byte)(sumMR);

                //        dataPtr += widthStep;
                //        //temina quando dataPtr = (0, height-1)

                //    }


                }
            }
        }

        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgCopy.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();

                int xdest = 0;
                int ydest = 0;

                double sumBx = 0;
                double sumGx = 0;
                double sumRx = 0; //soma das componentes no interior
                double sumBy = 0;
                double sumGy = 0;
                double sumRy = 0;

                double sumB = 0;
                double sumG = 0;
                double sumR = 0;

                double sumCBx = 0;
                double sumCGx = 0;
                double sumCRx = 0;
                double sumCBy = 0;
                double sumCGy = 0;
                double sumCRy = 0;

                double sumCB = 0;
                double sumCG = 0;
                double sumCR = 0;   //soma das componentes nos cantos

                double sumMBx = 0;
                double sumMGx = 0;
                double sumMRx = 0;
                double sumMBy = 0;
                double sumMGy = 0;
                double sumMRy = 0;

                double sumMB = 0;
                double sumMG = 0;
                double sumMR = 0; //soma das componentes nas margens

               
                if (nChan == 3)
                {
                    // ------------------------------------------|||---------------------------------------
                    //PROCESSAR O INTERIOR S/ MARGENS

                    dataPtr += nChan + widthStep; //pointer da imagem no ponto (1,1)
                    dataPtr[0] = 0;
                    dataPtr[1] = 255;
                    dataPtr[2] = 0;

                    //percorre cada linha de pixeis
                    for (ydest = 1; ydest < (height - 1); ydest++)

                    {
                        //percorre cada pixel de cada linha
                        for (xdest = 1; xdest < (width - 1); xdest++)

                        {
                            dataPtrAux = (xdest - 1) * nChan + (ydest - 1) * widthStep + dataPtrOrigem;
                            //Pointer no pixel (xdest-1,ydest-1) (0,0) - A

                           
                            sumBx = dataPtrAux[0];
                            sumGx = dataPtrAux[1];
                            sumRx = dataPtrAux[2];

                            sumBy = -dataPtrAux[0];
                            sumGy = -dataPtrAux[1];
                            sumRy = -dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (1,0) - B

                            sumBy -= 2 * dataPtrAux[0];
                            sumGy -= 2 * dataPtrAux[1];
                            sumRy -= 2 * dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (2,0) - C

                            sumBx -= dataPtrAux[0];
                            sumGx -= dataPtrAux[1];
                            sumRx -= dataPtrAux[2];

                            sumBy -= dataPtrAux[0];
                            sumGy -= dataPtrAux[1];
                            sumRy -= dataPtrAux[2];

                            dataPtrAux += widthStep - 2 * nChan;    //Pointer no pixel (0,1) - D

                            sumBx += 2 * dataPtrAux[0];
                            sumGx += 2 * dataPtrAux[1];
                            sumRx += 2 * dataPtrAux[2];

                            dataPtrAux += 2 * nChan;     //Pointer no pixel (2,1) - F

                            sumBx -= 2 * dataPtrAux[0];
                            sumGx -= 2 * dataPtrAux[1];
                            sumRx -= 2 * dataPtrAux[2];

                            dataPtrAux += widthStep - 2 * nChan;     //Pointer no pixel (0,2) - G

                            sumBx += dataPtrAux[0];
                            sumGx += dataPtrAux[1];
                            sumRx += dataPtrAux[2];

                            sumBy += dataPtrAux[0];
                            sumGy += dataPtrAux[1];
                            sumRy += dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (1,2) - H

                            sumBy += 2 * dataPtrAux[0];
                            sumGy += 2 * dataPtrAux[1];
                            sumRy += 2 * dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (2,2) - I

                            sumBx -= dataPtrAux[0];
                            sumGx -= dataPtrAux[1];
                            sumRx -= dataPtrAux[2];

                            sumBy += dataPtrAux[0];
                            sumGy += dataPtrAux[1];
                            sumRy += dataPtrAux[2];

                            sumB = Math.Abs(sumBx) + Math.Abs(sumBy);
                            sumG = Math.Abs(sumGx) + Math.Abs(sumGy);
                            sumR = Math.Abs(sumRx) + Math.Abs(sumRy);

                            //sumB = Math.Sqrt(sumBx * sumBx + sumBy * sumBy);
                            //sumG = Math.Sqrt(sumGx * sumGx + sumGy * sumGy);
                            //sumR = Math.Sqrt(sumRx * sumRx + sumRy * sumRy);

                            if (sumB > 255) { sumB = 255; }
                            if (sumG > 255) { sumG = 255; }
                            if (sumR > 255) { sumR = 255; }
                            
                            dataPtr[0] = (byte)(Math.Round(sumB));
                            dataPtr[1] = (byte)(Math.Round(sumG));
                            dataPtr[2] = (byte)(Math.Round(sumR));
                            
                            //termina no pixel (width-1,height-2) CHECK
                            dataPtr += nChan;
                        }

                        dataPtr += 2 * nChan + padding;
                       // CONFIRMA-SE QUE ESTAMOS A PERCORRER BEM O INTERIROR
                        //termina no pixel (1,height-1) CHECK
                    }

                    //------------------------------------------||| ---------------------------------------
                    //PROCESSAR OS CANTOS
                     //1º CANTO(0,0)

                    dataPtr -= (nChan + (height - 1) * widthStep);
                    //Pointer na imagem de origem no pixel(0, 0) CHECK

                    dataPtrAux = dataPtrOrigem;
                   // Pointer Auxiliar no pixel(0,0) - E

                    sumCBx =  3 * dataPtrAux[0];
                    sumCGx =  3 * dataPtrAux[1];
                    sumCRx =  3 * dataPtrAux[2];

                    sumCBy =  -3 * dataPtrAux[0];
                    sumCGy =  -3 * dataPtrAux[1];
                    sumCRy =  -3 * dataPtrAux[2];

                    dataPtrAux += nChan;
                   // Pointer Auxiliar no pixel(1,0) - F

                    sumCBx -= 3 * dataPtrAux[0];
                    sumCGx -= 3 * dataPtrAux[1];
                    sumCRx -= 3 * dataPtrAux[2];

                    sumCBy -= dataPtrAux[0];
                    sumCGy -= dataPtrAux[1];
                    sumCRy -= dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;
                    //Pointer Auxiliar no pixel(0,1) - H

                    sumCBx += dataPtrAux[0];
                    sumCGx += dataPtrAux[1];
                    sumCRx += dataPtrAux[2];

                    sumCBy += 3 * dataPtrAux[0];
                    sumCGy += 3 * dataPtrAux[1];
                    sumCRy += 3 * dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel(1,1) - I

                    sumCBx -= dataPtrAux[0];
                    sumCGx -= dataPtrAux[1];
                    sumCRx -= dataPtrAux[2];

                    sumCBy += dataPtrAux[0];
                    sumCGy += dataPtrAux[1];
                    sumCRy += dataPtrAux[2];

                    sumCB = Math.Abs(sumCBx) + Math.Abs(sumCBy);
                    sumCG = Math.Abs(sumCGx) + Math.Abs(sumCGy);
                    sumCR = Math.Abs(sumCRx) + Math.Abs(sumCRy);

                    if (sumCB > 255) { sumCB = 255; }
                    if (sumCG > 255) { sumCG = 255; }
                    if (sumCR > 255) { sumCR = 255; }

                    dataPtr[0] = (byte)(Math.Round(sumCB));
                    dataPtr[1] = (byte)(Math.Round(sumCG));
                    dataPtr[2] = (byte)(Math.Round(sumCR));

                    // ------------------------------------------|||---------------------------------------
                    // 2º CANTO (width-1,0) 

                    dataPtr += (width - 1) * nChan;
                    //Pointer na imagem principal no pixel (width-1,0)
                    dataPtrAux = (width - 2) * nChan + dataPtrOrigem;
                    //Pointer Auxiliar no pixel (width-2, 0) - D

                    sumCBx = 3 * dataPtrAux[0];
                    sumCGx = 3 * dataPtrAux[1];
                    sumCRx = 3 * dataPtrAux[2];

                    sumCBy =  -dataPtrAux[0];
                    sumCGy =  -dataPtrAux[1];
                    sumCRy =  -dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (width-1,0) - E

                    sumCBx -= 3 * dataPtrAux[0];
                    sumCGx -= 3 * dataPtrAux[1];
                    sumCRx -= 3 * dataPtrAux[2];

                    sumCBy -= 3 * dataPtrAux[0];
                    sumCGy -= 3 * dataPtrAux[1];
                    sumCRy -= 3 * dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;
                    //Pointer Auxiliar no pixel (width-2,1) - G

                    sumCBx += dataPtrAux[0];
                    sumCGx += dataPtrAux[1];
                    sumCRx += dataPtrAux[2];

                    sumCBy += dataPtrAux[0];
                    sumCGy += dataPtrAux[1];
                    sumCRy += dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (width-1,1) - H

                    sumCBx -= dataPtrAux[0];
                    sumCGx -= dataPtrAux[1];
                    sumCRx -= dataPtrAux[2];

                    sumCBy += 3 * dataPtrAux[0];
                    sumCGy += 3 * dataPtrAux[1];
                    sumCRy += 3 * dataPtrAux[2];

                    sumCB = Math.Abs(sumCBx) + Math.Abs(sumCBy);
                    sumCG = Math.Abs(sumCGx) + Math.Abs(sumCGy);
                    sumCR = Math.Abs(sumCRx) + Math.Abs(sumCRy);

                    if (sumCB > 255) { sumCB = 255; }
                    if (sumCG > 255) { sumCG = 255; }
                    if (sumCR > 255) { sumCR = 255; }

                    dataPtr[0] = (byte)(Math.Round(sumCB));
                    dataPtr[1] = (byte)(Math.Round(sumCG));
                    dataPtr[2] = (byte)(Math.Round(sumCR));

                    // ------------------------------------------|||---------------------------------------
                    // 3º CANTO (width-1,height-1) 

                    dataPtr += (height - 1) * widthStep;
                    //Pointer na imagem principal no pixel (width-1,height-1)
                    dataPtrAux = ((height - 2) * widthStep) + ((width - 2) * nChan) + dataPtrOrigem;
                    //Pointer Auxiliar no pixel (width-2, height-2) - A

                    sumCBx = dataPtrAux[0];
                    sumCGx = dataPtrAux[1];
                    sumCRx = dataPtrAux[2];

                    sumCBy = -dataPtrAux[0];
                    sumCGy = -dataPtrAux[1];
                    sumCRy = -dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (width-1,height-2) - B

                    sumCBx -= dataPtrAux[0];
                    sumCGx -= dataPtrAux[1];
                    sumCRx -= dataPtrAux[2];

                    sumCBy -= 3 * dataPtrAux[0];
                    sumCGy -= 3 * dataPtrAux[1];
                    sumCRy -= 3 * dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;
                    //Pointer Auxiliar no pixel (width-2,height-1) - D

                    sumCBx += 3 * dataPtrAux[0];
                    sumCGx += 3 * dataPtrAux[1];
                    sumCRx += 3 * dataPtrAux[2];

                    sumCBy += dataPtrAux[0];
                    sumCGy += dataPtrAux[1];
                    sumCRy += dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (width-1,height-1) - E

                    sumCBx -= 3 * dataPtrAux[0];
                    sumCGx -= 3 * dataPtrAux[1];
                    sumCRx -= 3 * dataPtrAux[2];

                    sumCBy += 3 * dataPtrAux[0];
                    sumCGy += 3 * dataPtrAux[1];
                    sumCRy += 3 * dataPtrAux[2];

                    sumCB = Math.Abs(sumCBx) + Math.Abs(sumCBy);
                    sumCG = Math.Abs(sumCGx) + Math.Abs(sumCGy);
                    sumCR = Math.Abs(sumCRx) + Math.Abs(sumCRy);

                    if (sumCB > 255) { sumCB = 255; }
                    if (sumCG > 255) { sumCG = 255; }
                    if (sumCR > 255) { sumCR = 255; }

                    dataPtr[0] = (byte)(Math.Round(sumCB));
                    dataPtr[1] = (byte)(Math.Round(sumCG));
                    dataPtr[2] = (byte)(Math.Round(sumCR));


                    // ------------------------------------------|||---------------------------------------
                    // 4º CANTO (0,height-1)

                    dataPtr -= (width - 1) * nChan;
                    //Pointer na imagem principal no pixel (0, height-1)
                    dataPtrAux = (height - 2) * widthStep + dataPtrOrigem;
                    //Pointer Auxiliar no pixel (0, height-2) - B

                    sumCBx = dataPtrAux[0];
                    sumCGx = dataPtrAux[1];
                    sumCRx = dataPtrAux[2];

                    sumCBy = - 3 * dataPtrAux[0];
                    sumCGy = - 3 * dataPtrAux[1];
                    sumCRy = - 3 * dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (1, height-2) - C

                    sumCBx -= dataPtrAux[0];
                    sumCGx -= dataPtrAux[1];
                    sumCRx -= dataPtrAux[2];

                    sumCBy -= dataPtrAux[0];
                    sumCGy -= dataPtrAux[1];
                    sumCRy -= dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;
                    //Pointer Auxiliar no pixel (0, height-1) - E

                    sumCBx += 3 * dataPtrAux[0];
                    sumCGx += 3* dataPtrAux[1];
                    sumCRx += 3 * dataPtrAux[2];

                    sumCBy += 3 * dataPtrAux[0];
                    sumCGy += 3 * dataPtrAux[1];
                    sumCRy += 3 * dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (1, height-1) - F

                    sumCBx -= 3 * dataPtrAux[0];
                    sumCGx -= 3 * dataPtrAux[1];
                    sumCRx -= 3 * dataPtrAux[2];

                    sumCBy += dataPtrAux[0];
                    sumCGy += dataPtrAux[1];
                    sumCRy += dataPtrAux[2];

                    sumCB = Math.Abs(sumCBx) + Math.Abs(sumCBy);
                    sumCG = Math.Abs(sumCGx) + Math.Abs(sumCGy);
                    sumCR = Math.Abs(sumCRx) + Math.Abs(sumCRy);


                    if (sumCB > 255) { sumCB = 255; }
                    if (sumCG > 255) { sumCG = 255; }
                    if (sumCR > 255) { sumCR = 255; }

                    dataPtr[0] = (byte)(Math.Round(sumCB));
                    dataPtr[1] = (byte)(Math.Round(sumCG));
                    dataPtr[2] = (byte)(Math.Round(sumCR));

                    //Pointer na imagem principal no pixel (0, height-1)

                    // ------------------------------------------|||---------------------------------------
                    // FINALMENTE PROCESSAMOS AS MARGENS S/ CANTOS
                    // MARGEM SUPERIOR (M1)

                    dataPtr -= ((height - 1) * widthStep) - nChan;
                    //Pointer na imagem principal no pixel (1,0)

                    //Pointer Auxiliar ao longo da linha y=0
                    for (xdest = 1; xdest < width - 1; xdest++)

                    {
                        dataPtrAux = ((xdest - 1) * nChan + dataPtrOrigem);
                        //pixel anterior ao processado - D

                        sumMBx = 3 * dataPtrAux[0];
                        sumMGx = 3 * dataPtrAux[1];
                        sumMRx = 3 * dataPtrAux[2];

                        sumMBy = -dataPtrAux[0];
                        sumMGy = -dataPtrAux[1];
                        sumMRy = -dataPtrAux[2];

                        //O proprio pixel - E

                        dataPtrAux = ((xdest) * nChan + dataPtrOrigem);

                        sumMBy -= 2 * dataPtrAux[0];
                        sumMGy -= 2 * dataPtrAux[1];
                        sumMRy -= 2 * dataPtrAux[2];

                        dataPtrAux = ((xdest + 1) * nChan + dataPtrOrigem);
                        //pixel posterior ao processado - F

                        sumMBx -= 3 * dataPtrAux[0];
                        sumMGx -= 3 * dataPtrAux[1];
                        sumMRx -= 3 * dataPtrAux[2];

                        sumMBy -= dataPtrAux[0];
                        sumMGy -= dataPtrAux[1];
                        sumMRy -= dataPtrAux[2];


                        dataPtrAux = (widthStep + (xdest - 1) * nChan + dataPtrOrigem);
                        //linha seguinte ao pixel a ser processado - G

                        sumMBx += dataPtrAux[0];
                        sumMGx += dataPtrAux[1];
                        sumMRx += dataPtrAux[2];

                        sumMBy += dataPtrAux[0];
                        sumMGy += dataPtrAux[1];
                        sumMRy += dataPtrAux[2];

                        dataPtrAux = (widthStep + (xdest) * nChan + dataPtrOrigem);
                        //H

                        sumMBy += 2 * dataPtrAux[0];
                        sumMGy += 2 * dataPtrAux[1];
                        sumMRy += 2 * dataPtrAux[2];

                        dataPtrAux = (widthStep + (xdest + 1) * nChan + dataPtrOrigem);
                        //I

                        sumMBx -= dataPtrAux[0];
                        sumMGx -= dataPtrAux[1];
                        sumMRx -= dataPtrAux[2];

                        sumMBy += dataPtrAux[0];
                        sumMGy += dataPtrAux[1];
                        sumMRy += dataPtrAux[2];

                        sumMB = Math.Abs(sumMBx) + Math.Abs(sumMBy);
                        sumMG = Math.Abs(sumMGx) + Math.Abs(sumMGy);
                        sumMR = Math.Abs(sumMRx) + Math.Abs(sumMRy);


                        if (sumMB > 255) { sumMB = 255; }
                        if (sumMG > 255) { sumMG = 255; }
                        if (sumMR > 255) { sumMR = 255; }

                        dataPtr[0] = (byte)(Math.Round(sumMB));
                        dataPtr[1] = (byte)(Math.Round(sumMG));
                        dataPtr[2] = (byte)(Math.Round(sumMR));

                        dataPtr += nChan; //termina quando dataPtr = (width-1 ,0) CHECK

                    }

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM LATERAL DIREITA (M2)

                    dataPtr += widthStep;
                    //Pointer na imagem principal no pixel (width-1,1)

                    //Pointer ao longo da coluna x=width-1
                    for (ydest = 1; ydest < height - 1; ydest++)

                    {
                        dataPtrAux = ((width - 1) * nChan + (ydest - 1) * widthStep + dataPtrOrigem);
                        //ir ao pixel acima do pixel a ser processado - B

                        sumMBx = -dataPtrAux[0];
                        sumMGx = -dataPtrAux[1];
                        sumMRx = -dataPtrAux[2];

                        sumMBy = -3 * dataPtrAux[0];
                        sumMGy = -3 * dataPtrAux[1];
                        sumMRy = -3 * dataPtrAux[2];

                        dataPtrAux = ((width - 1) * nChan + (ydest) * widthStep + dataPtrOrigem);
                        // E

                        sumMBx -= 2 * dataPtrAux[0];
                        sumMGx -= 2 * dataPtrAux[1];
                        sumMRx -= 2 * dataPtrAux[2];

                        dataPtrAux = ((width - 1) * nChan + (ydest + 1) * widthStep + dataPtrOrigem);
                        //ir ao pixel abaixo do pixel a ser processado H

                        sumMBx -= dataPtrAux[0];
                        sumMGx -= dataPtrAux[1];
                        sumMRx -= dataPtrAux[2];

                        sumMBy += 3 * dataPtrAux[0];
                        sumMGy += 3 * dataPtrAux[1];
                        sumMRy += 3 * dataPtrAux[2];


                        dataPtrAux = ((width - 2) * nChan + ((ydest - 1) * widthStep) + dataPtrOrigem);
                        //coluna anterior ao pixel a ser processado - A

                        sumMBx += dataPtrAux[0];
                        sumMGx += dataPtrAux[1];
                        sumMRx += dataPtrAux[2];

                        sumMBy -= dataPtrAux[0];
                        sumMGy -= dataPtrAux[1];
                        sumMRy -= dataPtrAux[2];

                        dataPtrAux = ((width - 2) * nChan + (ydest * widthStep) + dataPtrOrigem);
                        //D

                        sumMBx += 2 * dataPtrAux[0];
                        sumMGx += 2 * dataPtrAux[1];
                        sumMRx += 2 * dataPtrAux[2];

                        dataPtrAux = ((width - 2) * nChan + ((ydest + 1) * widthStep) + dataPtrOrigem);
                        //G
                        sumMBx += dataPtrAux[0];
                        sumMGx += dataPtrAux[1];
                        sumMRx += dataPtrAux[2];

                        sumMBy += dataPtrAux[0];
                        sumMGy += dataPtrAux[1];
                        sumMRy += dataPtrAux[2];

                        sumMB = Math.Abs(sumMBx) + Math.Abs(sumMBy);
                        sumMG = Math.Abs(sumMGx) + Math.Abs(sumMGy);
                        sumMR = Math.Abs(sumMRx) + Math.Abs(sumMRy);

                        if (sumMB > 255) { sumMB = 255; }
                        if (sumMG > 255) { sumMG = 255; }
                        if (sumMR > 255) { sumMR = 255; }

                        dataPtr[0] = (byte)(Math.Round(sumMB));
                        dataPtr[1] = (byte)(Math.Round(sumMG));
                        dataPtr[2] = (byte)(Math.Round(sumMR));

                        dataPtr += widthStep;
                        //temina quando dataPtr = (width-1, height-1)
                    }

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM INFERIOR (M3)

                    dataPtr -= (width - 2) * nChan;
                    //Pointer na imagem principal no pixel (1,height-1)

                    //Pointer ao longo da linha y=height-1
                    for (xdest = 1; xdest < width - 1; xdest++)

                    {
                        dataPtrAux = ((xdest - 1) * nChan + (height - 1) * widthStep + dataPtrOrigem);
                        //pixel anterior ao processado - D

                        sumMBx = 3 * dataPtrAux[0];
                        sumMGx = 3 * dataPtrAux[1];
                        sumMRx = 3 * dataPtrAux[2];

                        sumMBy =  dataPtrAux[0];
                        sumMGy =  dataPtrAux[1];
                        sumMRy =  dataPtrAux[2];

                        dataPtrAux = ((xdest) * nChan + (height - 1) * widthStep + dataPtrOrigem);
                        //E

                        sumMBy += 2 * dataPtrAux[0];
                        sumMGy += 2 * dataPtrAux[1];
                        sumMRy += 2 * dataPtrAux[2];

                        dataPtrAux = ((xdest + 1) * nChan + (height - 1) * widthStep + dataPtrOrigem);
                        //pixel posterior ao processado - F

                        sumMBx -= 3 * dataPtrAux[0];
                        sumMGx -= 3 * dataPtrAux[1];
                        sumMRx -= 3 * dataPtrAux[2];

                        sumMBy += dataPtrAux[0];
                        sumMGy += dataPtrAux[1];
                        sumMRy += dataPtrAux[2];


                        dataPtrAux = ((xdest - 1) * nChan + (height - 2) * widthStep + dataPtrOrigem);
                        //linha anterior ao pixel a ser processado - A

                        sumMBx +=  dataPtrAux[0];
                        sumMGx +=  dataPtrAux[1];
                        sumMRx +=  dataPtrAux[2];

                        sumMBy -= dataPtrAux[0];
                        sumMGy -= dataPtrAux[1];
                        sumMRy -= dataPtrAux[2];

                        dataPtrAux = ((xdest) * nChan + (height - 2) * widthStep + dataPtrOrigem);
                        //B

                        sumMBy -= 2 * dataPtrAux[0];
                        sumMGy -= 2 * dataPtrAux[1];
                        sumMRy -= 2 * dataPtrAux[2];

                        dataPtrAux = ((xdest + 1) * nChan + (height - 2) * widthStep + dataPtrOrigem);
                        //C

                        sumMBx -= dataPtrAux[0];
                        sumMGx -= dataPtrAux[1];
                        sumMRx -= dataPtrAux[2];

                        sumMBy -= dataPtrAux[0];
                        sumMGy -= dataPtrAux[1];
                        sumMRy -= dataPtrAux[2];

                        sumMB = Math.Abs(sumMBx) + Math.Abs(sumMBy);
                        sumMG = Math.Abs(sumMGx) + Math.Abs(sumMGy);
                        sumMR = Math.Abs(sumMRx) + Math.Abs(sumMRy);

                        if (sumMB > 255) { sumMB = 255; }
                        if (sumMG > 255) { sumMG = 255; }
                        if (sumMR > 255) { sumMR = 255; }

                        dataPtr[0] = (byte)(Math.Round(sumMB));
                        dataPtr[1] = (byte)(Math.Round(sumMG));
                        dataPtr[2] = (byte)(Math.Round(sumMR));

                        dataPtr += nChan; //termina quando dataPtr = (width-1 ,height -1) CHECK

                    }

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM LATERAL ESQUERDA (M4)

                    dataPtr -= ((width - 1) * nChan + (height - 2) * widthStep);
                    //Pointer na imagem principal no pixel (0,1)

                    //Pointer ao longo da coluna x=0
                    for (ydest = 1; ydest < height - 1; ydest++)

                    {
                        dataPtrAux = (ydest - 1) * widthStep + dataPtrOrigem;
                        //ir ao pixel acima do pixel a ser processado - B

                        sumMBx = dataPtrAux[0];
                        sumMGx =  dataPtrAux[1];
                        sumMRx =  dataPtrAux[2];

                        sumMBy = -3 * dataPtrAux[0];
                        sumMGy = -3 * dataPtrAux[1];
                        sumMRy = -3 * dataPtrAux[2];

                        dataPtrAux = (ydest) * widthStep + dataPtrOrigem;
                        //E

                        sumMBx += 2 * dataPtrAux[0];
                        sumMGx += 2 * dataPtrAux[1];
                        sumMRx += 2 * dataPtrAux[2];

                        dataPtrAux = (ydest + 1) * widthStep + dataPtrOrigem;
                        //ir ao pixel abaixo do pixel a ser processado - H

                        sumMBx +=  dataPtrAux[0];
                        sumMGx += dataPtrAux[1];
                        sumMRx += dataPtrAux[2];

                        sumMBy += 3 * dataPtrAux[0];
                        sumMGy += 3 * dataPtrAux[1];
                        sumMRy += 3 * dataPtrAux[2];


                        dataPtrAux = nChan + ((ydest - 1) * widthStep) + dataPtrOrigem;
                        //coluna seguinte ao pixel a ser processado - C

                        sumMBx -= dataPtrAux[0];
                        sumMGx -= dataPtrAux[1];
                        sumMRx -= dataPtrAux[2];

                        sumMBy -= dataPtrAux[0];
                        sumMGy -= dataPtrAux[1];
                        sumMRy -= dataPtrAux[2];

                        dataPtrAux = nChan + (ydest * widthStep) + dataPtrOrigem;
                        //F

                        sumMBx -= 2 * dataPtrAux[0];
                        sumMGx -= 2 * dataPtrAux[1];
                        sumMRx -= 2 * dataPtrAux[2];

                        dataPtrAux = nChan + ((ydest + 1) * widthStep) + dataPtrOrigem; //coluna anterior ao pixel a ser processado

                        sumMBx -= dataPtrAux[0];
                        sumMGx -= dataPtrAux[1];
                        sumMRx -= dataPtrAux[2];

                        sumMBy += dataPtrAux[0];
                        sumMGy += dataPtrAux[1];
                        sumMRy += dataPtrAux[2];

                        sumMB = Math.Abs(sumMBx) + Math.Abs(sumMBy);
                        sumMG = Math.Abs(sumMGx) + Math.Abs(sumMGy);
                        sumMR = Math.Abs(sumMRx) + Math.Abs(sumMRy);

                        if (sumMB > 255) { sumMB = 255; }
                        if (sumMG > 255) { sumMG = 255; }
                        if (sumMR > 255) { sumMR = 255; }

                        dataPtr[0] = (byte)(Math.Round(sumMB));
                        dataPtr[1] = (byte)(Math.Round(sumMG));
                        dataPtr[2] = (byte)(Math.Round(sumMR));

                        dataPtr += widthStep;
                        //temina quando dataPtr = (0, height-1)

                    }


                }
            }

        }

        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgCopy.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();

                int xdest = 0;
                int ydest = 0;

                double sumB = 0;
                double sumG = 0;
                double sumR = 0;

                double Bvalue = 0;
                double Gvalue = 0;
                double Rvalue = 0;

                double sumCB = 0;
                double sumCG = 0;
                double sumCR = 0;

                double sumMB = 0;
                double sumMG = 0;
                double sumMR = 0; //soma das componentes nas margens

                float b1 = matrix[0,0];
                float b2 = matrix[1,0];
                float b3 = matrix[2,0];
                float b4 = matrix[0,1];
                float b5 = matrix[1,1];
                float b6 = matrix[2,1];
                float b7 = matrix[0,2];
                float b8 = matrix[1,2];
                float b9 = matrix[2,2];

                if (nChan == 3)
                {
                    // ------------------------------------------|||---------------------------------------
                    //PROCESSAR O INTERIOR S/ MARGENS

                    dataPtr += nChan + widthStep; //pointer da imagem no ponto (1,1)

                    //percorre cada linha de pixeis
                    for (ydest = 1; ydest < height - 1; ydest++)

                    {
                        //percorre cada pixel de cada linha
                        for (xdest = 1; xdest < width - 1; xdest++)

                        {
                            dataPtrAux = (xdest - 1) * nChan + (ydest - 1) * widthStep + dataPtrOrigem;
                            //Pointer para calculo da média no pixel (xdest-1,ydest-1)

                            sumB = b1 * dataPtrAux[0];
                            sumG = b1 * dataPtrAux[1];
                            sumR = b1 * dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (1,0)

                            sumB += b2 * dataPtrAux[0];
                            sumG += b2 * dataPtrAux[1];
                            sumR += b2 * dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (2,0)

                            sumB += b3 * dataPtrAux[0];
                            sumG += b3 * dataPtrAux[1];
                            sumR += b3 * dataPtrAux[2];

                            dataPtrAux += widthStep - 2 * nChan;    //Pointer no pixel (0,1)

                            sumB += b4 * dataPtrAux[0];
                            sumG += b4 * dataPtrAux[1];
                            sumR += b4 * dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (1,1)

                            sumB += b5 * dataPtrAux[0];
                            sumG += b5 * dataPtrAux[1];
                            sumR += b5 * dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (2,1)

                            sumB += b6 * dataPtrAux[0];
                            sumG += b6 * dataPtrAux[1];
                            sumR += b6 * dataPtrAux[2];

                            dataPtrAux += widthStep - 2 * nChan;     //Pointer no pixel (0,2)

                            sumB += b7 * dataPtrAux[0];
                            sumG += b7 * dataPtrAux[1];
                            sumR += b7 * dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (1,2)

                            sumB += b8 * dataPtrAux[0];
                            sumG += b8 * dataPtrAux[1];
                            sumR += b8 * dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (2,2)

                            sumB += b9 * dataPtrAux[0];
                            sumG += b9 * dataPtrAux[1];
                            sumR += b9 * dataPtrAux[2];

                            Bvalue = Math.Round(sumB / matrixWeight);
                            Gvalue = Math.Round(sumG / matrixWeight);
                            Rvalue = Math.Round(sumR / matrixWeight);

                            if (Bvalue < 0) { Bvalue = 0; }
                            if (Gvalue < 0) { Gvalue = 0; }
                            if (Rvalue < 0) { Rvalue = 0; }
                            if (Bvalue > 255) { Bvalue = 255; }
                            if (Gvalue > 255) { Gvalue = 255; }
                            if (Rvalue > 255) { Rvalue = 255; }


                            dataPtr[0] = (byte)(Bvalue);
                            dataPtr[1] = (byte)(Gvalue);
                            dataPtr[2] = (byte)(Rvalue);

                            dataPtr += nChan; //termina no pixel (width-1,height-2)

                        }

                        dataPtr += 2 * nChan + padding;
                    }
                    //termina no pixel (1,height-1) CHECK

                    // ------------------------------------------|||---------------------------------------
                    // PROCESSAR OS CANTOS
                    // 1º CANTO (0,0)

                    dataPtr -= (nChan + (height - 1) * widthStep);
                    //Pointer na imagem de origem no pixel (0,0) CHECK
                   
                    dataPtrAux = dataPtrOrigem;
                    //Pointer Auxiliar no pixel (0,0)

                    sumCB = (b1 + b2 + b4 + b5) * dataPtrAux[0];
                    sumCG = (b1 + b2 + b4 + b5) * dataPtrAux[1];
                    sumCR = (b1 + b2 + b4 + b5) * dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (1,0)

                    sumCB += (b3 + b6) * dataPtrAux[0];
                    sumCG += (b3 + b6) * dataPtrAux[1];
                    sumCR += (b3 + b6) * dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;
                    //Pointer Auxiliar no pixel (0,1)

                    sumCB += (b7 + b8) * dataPtrAux[0];
                    sumCG += (b7 + b8) * dataPtrAux[1];
                    sumCR += (b7 + b8) * dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (1,1)

                    sumCB += b9 + dataPtrAux[0];
                    sumCG += b9 + dataPtrAux[1];
                    sumCR += b9 + dataPtrAux[2];

                    Bvalue = Math.Round(sumCB / matrixWeight);
                    Gvalue = Math.Round(sumCG / matrixWeight);
                    Rvalue = Math.Round(sumCR / matrixWeight);

                    if (Bvalue < 0) { Bvalue = 0; }
                    if (Gvalue < 0) { Gvalue = 0; }
                    if (Rvalue < 0) { Rvalue = 0; }
                    if (Bvalue > 255) { Bvalue = 255; }
                    if (Gvalue > 255) { Gvalue = 255; }
                    if (Rvalue > 255) { Rvalue = 255; }
                    
                    dataPtr[0] = (byte)(Bvalue);
                    dataPtr[1] = (byte)(Gvalue);
                    dataPtr[2] = (byte)(Rvalue);


                    // ------------------------------------------|||---------------------------------------
                    // 2º CANTO (width-1,0)

                    dataPtr += (width - 1) * nChan;
                    //Pointer na imagem principal no pixel (width-1,0)
                    dataPtrAux = (width - 2) * nChan + dataPtrOrigem;
                    //Pointer Auxiliar no pixel (width-2, 0)

                    sumCB = (b1 + b4) * dataPtrAux[0];
                    sumCG = (b1 + b4) * dataPtrAux[1];
                    sumCR = (b1 + b4) * dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (width-1,0)

                    sumCB += (b2 + b3 + b5 + b6) * dataPtrAux[0];
                    sumCG += (b2 + b3 + b5 + b6) * dataPtrAux[1];
                    sumCR += (b2 + b3 + b5 + b6) * dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;
                    //Pointer Auxiliar no pixel (width-2,1)

                    sumCB += b7 * dataPtrAux[0];
                    sumCG += b7 * dataPtrAux[1];
                    sumCR += b7 * dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (width-1,1)

                    sumCB += (b8 + b9) * dataPtrAux[0];
                    sumCG += (b8 + b9) * dataPtrAux[1];
                    sumCR += (b8 + b9) * dataPtrAux[2];

                    Bvalue = Math.Round(sumCB / matrixWeight);
                    Gvalue = Math.Round(sumCG / matrixWeight);
                    Rvalue = Math.Round(sumCR / matrixWeight);

                    if (Bvalue < 0) { Bvalue = 0; }
                    if (Gvalue < 0) { Gvalue = 0; }
                    if (Rvalue < 0) { Rvalue = 0; }
                    if (Bvalue > 255) { Bvalue = 255; }
                    if (Gvalue > 255) { Gvalue = 255; }
                    if (Rvalue > 255) { Rvalue = 255; }

                    dataPtr[0] = (byte)(Bvalue);
                    dataPtr[1] = (byte)(Gvalue);
                    dataPtr[2] = (byte)(Rvalue);

                    // ------------------------------------------|||---------------------------------------
                    // 3º CANTO (width-1,height-1)

                    dataPtr += (height - 1) * widthStep;
                    //Pointer na imagem principal no pixel (width-1,height-1)
                    dataPtrAux = ((height - 2) * widthStep) + ((width - 2) * nChan) + dataPtrOrigem;
                    //Pointer Auxiliar no pixel (width-2, height-2)

                    sumCB = b1 * dataPtrAux[0];
                    sumCG = b1 * dataPtrAux[1];
                    sumCR = b1 * dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (width-1,height-2)

                    sumCB += (b2 + b3) * dataPtrAux[0];
                    sumCG += (b2 + b3) * dataPtrAux[1];
                    sumCR += (b2 + b3) * dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;
                    //Pointer Auxiliar no pixel (width-2,height-1)

                    sumCB += (b4 + b7) * dataPtrAux[0];
                    sumCG += (b4 + b7) * dataPtrAux[1];
                    sumCR += (b4 + b7) * dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (width-1,height-1)

                    sumCB += (b5 + b6 + b8 + b9) * dataPtrAux[0];
                    sumCG += (b5 + b6 + b8 + b9) * dataPtrAux[1];
                    sumCR += (b5 + b6 + b8 + b9) * dataPtrAux[2];

                    Bvalue = Math.Round(sumCB / matrixWeight);
                    Gvalue = Math.Round(sumCG / matrixWeight);
                    Rvalue = Math.Round(sumCR / matrixWeight);

                    if (Bvalue < 0) { Bvalue = 0; }
                    if (Gvalue < 0) { Gvalue = 0; }
                    if (Rvalue < 0) { Rvalue = 0; }
                    if (Bvalue > 255) { Bvalue = 255; }
                    if (Gvalue > 255) { Gvalue = 255; }
                    if (Rvalue > 255) { Rvalue = 255; }

                    dataPtr[0] = (byte)(Bvalue);
                    dataPtr[1] = (byte)(Gvalue);
                    dataPtr[2] = (byte)(Rvalue);


                    // ------------------------------------------|||---------------------------------------
                    // 4º CANTO (0,height-1)

                    dataPtr -= (width - 1) * nChan;
                    //Pointer na imagem principal no pixel (0, height-1)
                    dataPtrAux = (height - 2) * widthStep + dataPtrOrigem;
                    //Pointer Auxiliar no pixel (0, height-2)

                    sumCB = (b1 + b2) * dataPtrAux[0];
                    sumCG = (b1 + b2) * dataPtrAux[1];
                    sumCR = (b1 + b2) * dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (1, height-2)

                    sumCB += b3 * dataPtrAux[0];
                    sumCG += b3 * dataPtrAux[1];
                    sumCR += b3 * dataPtrAux[2];

                    dataPtrAux += widthStep - nChan;
                    //Pointer Auxiliar no pixel (0, height-1)

                    sumCB += (b4 + b5 + b7 + b8) * dataPtrAux[0];
                    sumCG += (b4 + b5 + b7 + b8) * dataPtrAux[1];
                    sumCR += (b4 + b5 + b7 + b8) * dataPtrAux[2];

                    dataPtrAux += nChan;
                    //Pointer Auxiliar no pixel (1, height-1)

                    sumCB += (b6 + b9) * dataPtrAux[0];
                    sumCG += (b6 + b9) * dataPtrAux[1];
                    sumCR += (b6 + b9) * dataPtrAux[2];

                    Bvalue = Math.Round(sumCB / matrixWeight);
                    Gvalue = Math.Round(sumCG / matrixWeight);
                    Rvalue = Math.Round(sumCR / matrixWeight);

                    if (Bvalue < 0) { Bvalue = 0; }
                    if (Gvalue < 0) { Gvalue = 0; }
                    if (Rvalue < 0) { Rvalue = 0; }
                    if (Bvalue > 255) { Bvalue = 255; }
                    if (Gvalue > 255) { Gvalue = 255; }
                    if (Rvalue > 255) { Rvalue = 255; }

                    dataPtr[0] = (byte)(Bvalue);
                    dataPtr[1] = (byte)(Gvalue);
                    dataPtr[2] = (byte)(Rvalue);

                    //Pointer na imagem principal no pixel (0, height-1)

                    // ------------------------------------------|||---------------------------------------
                    // FINALMENTE PROCESSAMOS AS MARGENS S/ CANTOS
                    // MARGEM SUPERIOR (M1)

                    dataPtr -= ((height - 1) * widthStep) - nChan;
                    //Pointer na imagem principal no pixel (1,0)

                    //Pointer Auxiliar ao longo da linha y=0
                    for (xdest = 1; xdest < width - 1; xdest++)

                    {
                        dataPtrAux = ((xdest - 1) * nChan + dataPtrOrigem);
                        //pixel anterior ao processado

                        sumMB = (b1 + b4) * dataPtrAux[0];
                        sumMG = (b1 + b4) * dataPtrAux[1];
                        sumMR = (b1 + b4) * dataPtrAux[2];

                        dataPtrAux = ((xdest) * nChan + dataPtrOrigem);

                        sumMB += (b5 + b2) * dataPtrAux[0];
                        sumMG += (b5 + b2) * dataPtrAux[1];
                        sumMR += (b5 + b2) * dataPtrAux[2];

                        dataPtrAux = ((xdest + 1) * nChan + dataPtrOrigem);
                        //pixel posterior ao processado

                        sumMB += (b3 + b6) * dataPtrAux[0];
                        sumMG += (b3 + b6) * dataPtrAux[1];
                        sumMR += (b3 + b6) * dataPtrAux[2];


                        dataPtrAux = (widthStep + (xdest - 1) * nChan + dataPtrOrigem);
                        //linha seguinte ao pixel a ser processado

                        sumMB += b7 * dataPtrAux[0];
                        sumMG += b7 * dataPtrAux[1];
                        sumMR += b7 * dataPtrAux[2];

                        dataPtrAux = (widthStep + (xdest) * nChan + dataPtrOrigem);

                        sumMB += b8 * dataPtrAux[0];
                        sumMG += b8 * dataPtrAux[1];
                        sumMR += b8 * dataPtrAux[2];

                        dataPtrAux = (widthStep + (xdest + 1) * nChan + dataPtrOrigem);

                        sumMB += b9 * dataPtrAux[0];
                        sumMG += b9 * dataPtrAux[1];
                        sumMR += b9 * dataPtrAux[2];

                        Bvalue = Math.Round(sumCB / matrixWeight);
                        Gvalue = Math.Round(sumCG / matrixWeight);
                        Rvalue = Math.Round(sumCR / matrixWeight);

                        if (Bvalue < 0) { Bvalue = 0; }
                        if (Gvalue < 0) { Gvalue = 0; }
                        if (Rvalue < 0) { Rvalue = 0; }
                        if (Bvalue > 255) { Bvalue = 255; }
                        if (Gvalue > 255) { Gvalue = 255; }
                        if (Rvalue > 255) { Rvalue = 255; }

                        dataPtr[0] = (byte)(Bvalue);
                        dataPtr[1] = (byte)(Gvalue);
                        dataPtr[2] = (byte)(Rvalue);

                        dataPtr += nChan; //termina quando dataPtr = (width-1 ,0) CHECK

                    }

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM LATERAL DIREITA (M2)

                    dataPtr += widthStep;
                    //Pointer na imagem principal no pixel (width-1,1)

                    //Pointer ao longo da coluna x=width-1
                    for (ydest = 1; ydest < height - 1; ydest++)

                    {
                        dataPtrAux = ((width - 1) * nChan + (ydest - 1) * widthStep + dataPtrOrigem);
                        //ir ao pixel acima do pixel a ser processado

                        sumMB = (b2 + b3) * dataPtrAux[0];
                        sumMG = (b2 + b3) * dataPtrAux[1];
                        sumMR = (b2 + b3) * dataPtrAux[2];

                        dataPtrAux = ((width - 1) * nChan + (ydest) * widthStep + dataPtrOrigem);

                        sumMB += (b5 + b6) * dataPtrAux[0];
                        sumMG += (b5 + b6) * dataPtrAux[1];
                        sumMR += (b5 + b6) * dataPtrAux[2];

                        dataPtrAux = ((width - 1) * nChan + (ydest + 1) * widthStep + dataPtrOrigem);
                        //ir ao pixel abaixo do pixel a ser processado

                        sumMB += (b8 + b9) * dataPtrAux[0];
                        sumMG += (b8 + b9) * dataPtrAux[1];
                        sumMR += (b8 + b9) * dataPtrAux[2];


                        dataPtrAux = ((width - 2) * nChan + ((ydest - 1) * widthStep) + dataPtrOrigem);
                        //coluna anterior ao pixel a ser processado

                        sumMB += b1 * dataPtrAux[0];
                        sumMG += b1 * dataPtrAux[1];
                        sumMR += b1 * dataPtrAux[2];

                        dataPtrAux = ((width - 2) * nChan + (ydest * widthStep) + dataPtrOrigem);

                        sumMB += b4 * dataPtrAux[0];
                        sumMG += b4 * dataPtrAux[1];
                        sumMR += b4 * dataPtrAux[2];

                        dataPtrAux = ((width - 2) * nChan + ((ydest + 1) * widthStep) + dataPtrOrigem); //coluna anterior ao pixel a ser processado

                        sumMB += b7 * dataPtrAux[0];
                        sumMG += b7 * dataPtrAux[1];
                        sumMR += b7 * dataPtrAux[2];

                        Bvalue = Math.Round(sumCB / matrixWeight);
                        Gvalue = Math.Round(sumCG / matrixWeight);
                        Rvalue = Math.Round(sumCR / matrixWeight);

                        if (Bvalue < 0) { Bvalue = 0; }
                        if (Gvalue < 0) { Gvalue = 0; }
                        if (Rvalue < 0) { Rvalue = 0; }
                        if (Bvalue > 255) { Bvalue = 255; }
                        if (Gvalue > 255) { Gvalue = 255; }
                        if (Rvalue > 255) { Rvalue = 255; }

                        dataPtr[0] = (byte)(Bvalue);
                        dataPtr[1] = (byte)(Gvalue);
                        dataPtr[2] = (byte)(Rvalue);

                        dataPtr += widthStep;
                        //temina quando dataPtr = (width-1, height-1)
                    }

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM INFERIOR (M3)

                    dataPtr -= (width - 2) * nChan;
                    //Pointer na imagem principal no pixel (1,height-1)

                    //Pointer ao longo da linha y=height-1
                    for (xdest = 1; xdest < width - 1; xdest++)

                    {
                        dataPtrAux = ((xdest - 1) * nChan + (height - 1) * widthStep + dataPtrOrigem);
                        //pixel anterior ao processado

                        sumMB = (b7 + b4) * dataPtrAux[0];
                        sumMG = (b7 + b4) * dataPtrAux[1];
                        sumMR = (b7 + b4) * dataPtrAux[2];

                        dataPtrAux = ((xdest) * nChan + (height - 1) * widthStep + dataPtrOrigem);

                        sumMB += (b5 + b8) * dataPtrAux[0];
                        sumMG += (b5 + b8) * dataPtrAux[1];
                        sumMR += (b5 + b8) * dataPtrAux[2];

                        dataPtrAux = ((xdest + 1) * nChan + (height - 1) * widthStep + dataPtrOrigem);
                        //pixel posterior ao processado

                        sumMB += (b9 + b6) * dataPtrAux[0];
                        sumMG += (b9 + b6) * dataPtrAux[1];
                        sumMR += (b9 + b6) * dataPtrAux[2];


                        dataPtrAux = ((xdest - 1) * nChan + (height - 2) * widthStep + dataPtrOrigem);
                        //linha anterior ao pixel a ser processado

                        sumMB += b1 * dataPtrAux[0];
                        sumMG += b1 * dataPtrAux[1];
                        sumMR += b1 * dataPtrAux[2];

                        dataPtrAux = ((xdest) * nChan + (height - 2) * widthStep + dataPtrOrigem);

                        sumMB += b2 * dataPtrAux[0];
                        sumMG += b2 * dataPtrAux[1];
                        sumMR += b2 * dataPtrAux[2];

                        dataPtrAux = ((xdest + 1) * nChan + (height - 2) * widthStep + dataPtrOrigem);

                        sumMB += b3 * dataPtrAux[0];
                        sumMG += b3 * dataPtrAux[1];
                        sumMR += b3 * dataPtrAux[2];

                        Bvalue = Math.Round(sumCB / matrixWeight);
                        Gvalue = Math.Round(sumCG / matrixWeight);
                        Rvalue = Math.Round(sumCR / matrixWeight);

                        if (Bvalue < 0) { Bvalue = 0; }
                        if (Gvalue < 0) { Gvalue = 0; }
                        if (Rvalue < 0) { Rvalue = 0; }
                        if (Bvalue > 255) { Bvalue = 255; }
                        if (Gvalue > 255) { Gvalue = 255; }
                        if (Rvalue > 255) { Rvalue = 255; }

                        dataPtr[0] = (byte)(Bvalue);
                        dataPtr[1] = (byte)(Gvalue);
                        dataPtr[2] = (byte)(Rvalue);

                        dataPtr += nChan; //termina quando dataPtr = (width-1 ,height -1) CHECK

                    }

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM LATERAL ESQUERDA (M4)

                    dataPtr -= ((width - 1) * nChan + (height - 2) * widthStep);
                    //Pointer na imagem principal no pixel (0,1)

                    //Pointer ao longo da coluna x=0
                    for (ydest = 1; ydest < height - 1; ydest++)

                    {
                        dataPtrAux = (ydest - 1) * widthStep + dataPtrOrigem;
                        //ir ao pixel acima do pixel a ser processado

                        sumMB = (b2 + b1) * dataPtrAux[0];
                        sumMG = (b2 + b1) * dataPtrAux[1];
                        sumMR = (b2 + b1) * dataPtrAux[2];

                        dataPtrAux = (ydest) * widthStep + dataPtrOrigem;

                        sumMB += (b5 + b4) * dataPtrAux[0];
                        sumMG += (b5 + b4) * dataPtrAux[1];
                        sumMR += (b5 + b4) * dataPtrAux[2];

                        dataPtrAux = (ydest + 1) * widthStep + dataPtrOrigem;
                        //ir ao pixel abaixo do pixel a ser processado

                        sumMB += (b8 + b7) * dataPtrAux[0];
                        sumMG += (b8 + b7) * dataPtrAux[1];
                        sumMR += (b8 + b7) * dataPtrAux[2];


                        dataPtrAux = (width * nChan + ((ydest - 1) * widthStep) + dataPtrOrigem);
                        //coluna seguinte ao pixel a ser processado

                        sumMB += b3 * dataPtrAux[0];
                        sumMG += b3 * dataPtrAux[1];
                        sumMR += b3 * dataPtrAux[2];

                        dataPtrAux = (width * nChan + (ydest * widthStep) + dataPtrOrigem);

                        sumMB += b6 * dataPtrAux[0];
                        sumMG += b6 * dataPtrAux[1];
                        sumMR += b6 * dataPtrAux[2];

                        dataPtrAux = (width * nChan + ((ydest + 1) * widthStep) + dataPtrOrigem); //coluna anterior ao pixel a ser processado

                        sumMB += b9 * dataPtrAux[0];
                        sumMG += b9 * dataPtrAux[1];
                        sumMR += b9 * dataPtrAux[2];

                        Bvalue = Math.Round(sumCB / matrixWeight);
                        Gvalue = Math.Round(sumCG / matrixWeight);
                        Rvalue = Math.Round(sumCR / matrixWeight);

                        if (Bvalue < 0) { Bvalue = 0; }
                        if (Gvalue < 0) { Gvalue = 0; }
                        if (Rvalue < 0) { Rvalue = 0; }
                        if (Bvalue > 255) { Bvalue = 255; }
                        if (Gvalue > 255) { Gvalue = 255; }
                        if (Rvalue > 255) { Rvalue = 255; }

                        dataPtr[0] = (byte)(Bvalue);
                        dataPtr[1] = (byte)(Gvalue);
                        dataPtr[2] = (byte)(Rvalue);

                        dataPtr += widthStep;
                        //temina quando dataPtr = (0, height-1)

                    }


                }
            }

        }

        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {

            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgCopy.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();

                int xdest = 0;
                int ydest = 0;

                double pixelAB = 0;
                double pixelAG = 0;
                double pixelAR = 0; //soma das componentes no interior
                double pixelBB = 0;
                double pixelBG = 0;
                double pixelBR = 0;
                double pixelCB = 0;
                double pixelCG = 0;
                double pixelCR = 0;

                double sumB = 0;
                double sumG = 0;
                double sumR = 0;
                
                if (nChan == 3)
                {
                    // ------------------------------------------|||---------------------------------------
                    //PROCESSAR O INTERIOR S/ MARGENS

                    //pointer em (0,0)
                    
                    //percorre cada linha de pixeis
                    for (ydest = 0; ydest < (height - 1); ydest++)

                    {
                        //percorre cada pixel de cada linha
                        for (xdest = 0; xdest < (width - 1); xdest++)

                        {
                            
                            dataPtrAux = xdest * nChan + ydest * widthStep + dataPtrOrigem;
                            //Pointer no pixel (xdest, ydest) - A

                            pixelAB = dataPtrAux[0];
                            pixelAG = dataPtrAux[1];
                            pixelAR = dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (xdest+1,ydest) - B

                            pixelBB = dataPtrAux[0];
                            pixelBG = dataPtrAux[1];
                            pixelBR = dataPtrAux[2];

                            dataPtrAux += widthStep - nChan;     //Pointer no pixel (xdest,ydest+1) - C

                            pixelCB = dataPtrAux[0];
                            pixelCG = dataPtrAux[1];
                            pixelCR = dataPtrAux[2];


                            sumB = Math.Abs(pixelAB - pixelBB) + Math.Abs(pixelAB - pixelCB);
                            sumG = Math.Abs(pixelAG - pixelBG) + Math.Abs(pixelAG - pixelCG);
                            sumR = Math.Abs(pixelAR - pixelBR) + Math.Abs(pixelAR - pixelCR);


                            if (sumB > 255) { sumB = 255; }
                            if (sumG > 255) { sumG = 255; }
                            if (sumR > 255) { sumR = 255; }

                            dataPtr[0] = (byte)(Math.Round(sumB));
                            dataPtr[1] = (byte)(Math.Round(sumG));
                            dataPtr[2] = (byte)(Math.Round(sumR));

                            //termina no pixel (width-1,height-2) CHECK
                            dataPtr += nChan;
                            

                        }

                        dataPtr += nChan + padding;
                        // CONFIRMA-SE QUE ESTAMOS A PERCORRER BEM O INTERIROR
                        //termina no pixel (0,height-1) CHECK
                        
                    }

                    //------------------------------------------||| ---------------------------------------
                    //PROCESSAR OS CANTOS

                    //1º CANTO(0,0) NÃO NECESSITA DE PROCESSAMENTO (ACONTECE NO INTERIOR S/ MARGENS)

                    //2º CANTO(width-1,0) NÃO NECESSITA DE PROCESSAMENTO (ACONTECE NA MARGEM M2)
                    
                    // ------------------------------------------|||---------------------------------------
                    // 3º CANTO(width-1,height-1)

                    dataPtr += (width-1)*nChan;
                    //Pointer na imagem principal no pixel (width-1,height-1)
                    
                    dataPtr[0] = 0;
                    dataPtr[1] = 0;
                    dataPtr[2] = 0;

                    // ------------------------------------------|||---------------------------------------
                    // 4º CANTO(0,height-1) NÃO NECESSITA DE PROCESSAMENTO (ACONTECE NA MARGEM M3)

                    // ------------------------------------------|||---------------------------------------
                    // FINALMENTE PROCESSAMOS AS MARGENS S/ CANTOS
                    // MARGEM SUPERIOR (M1) NÃO NECESSITA DE PROCESSAMENTO (ACONTECE NO INTERIOR S/MARGENS)

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM LATERAL DIREITA (M2) + 2º CANTO

                    dataPtr -= (height-1)*widthStep;
                    //Pointer na imagem principal no pixel (width-1,0)

                    //Pointer ao longo da coluna x=width-1
                    for (ydest = 0; ydest < height - 1; ydest++)

                    {
                        dataPtrAux = ((width - 1) * nChan + ydest * widthStep + dataPtrOrigem);
                        //ir ao pixel a ser processado - A

                        pixelAB = dataPtrAux[0];
                        pixelAG = dataPtrAux[1];
                        pixelAR = dataPtrAux[2];

                        dataPtrAux += widthStep;     //Pointer no pixel (1,height-1) - C

                        pixelCB = dataPtrAux[0];
                        pixelCG = dataPtrAux[1];    
                        pixelCR = dataPtrAux[2];
                        
                        sumB = Math.Abs(pixelAB - pixelCB);
                        sumG = Math.Abs(pixelAG - pixelCG);
                        sumR = Math.Abs(pixelAR - pixelCR);

                        if (sumB > 255) { sumB = 255; }
                        if (sumG > 255) { sumG = 255; }
                        if (sumR > 255) { sumR = 255; }

                        dataPtr[0] = (byte)(Math.Round(sumB));
                        dataPtr[1] = (byte)(Math.Round(sumG));
                        dataPtr[2] = (byte)(Math.Round(sumR));

                        dataPtr += widthStep;
                        //temina quando dataPtr = (width-1, height-1)
                    }

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM INFERIOR (M3)

                    dataPtr -= (width - 1) * nChan;
                    //Pointer na imagem principal no pixel (1,height-1)

                    //Pointer ao longo da linha y=height-1
                    for (xdest = 0; xdest < width - 1; xdest++)

                    {
                        dataPtrAux = ((xdest) * nChan + (height - 1) * widthStep + dataPtrOrigem);
                        //pixel a ser processado - A

                        pixelAB = dataPtrAux[0];
                        pixelAG = dataPtrAux[1];
                        pixelAR = dataPtrAux[2];

                        dataPtrAux += nChan;     //Pointer no pixel (xdest+1,ydest) - B

                        pixelBB = dataPtrAux[0];
                        pixelBG = dataPtrAux[1];
                        pixelBR = dataPtrAux[2];

                        sumB = Math.Abs(pixelAB - pixelBB);
                        sumG = Math.Abs(pixelAG - pixelBG);
                        sumR = Math.Abs(pixelAR - pixelBR);

                        if (sumB > 255) { sumB = 255; }
                        if (sumG > 255) { sumG = 255; }
                        if (sumR > 255) { sumR = 255; }

                        dataPtr[0] = (byte)(Math.Round(sumB));
                        dataPtr[1] = (byte)(Math.Round(sumG));
                        dataPtr[2] = (byte)(Math.Round(sumR));

                        dataPtr += nChan; //termina quando dataPtr = (width-1 ,height -1) CHECK

                    }

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM LATERAL ESQUERDA (M4) NÃO NECESSITA DE PROCESSAMENTO (ACONTECE NO INTERIOR S/MARGENS)


                }

               


            }
                      

        }

        public static void Roberts(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy) {

            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage morigem = imgCopy.MIplImage;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte* dataPtrOrigem = (byte*)morigem.imageData.ToPointer();

                int xdest = 0;
                int ydest = 0;

                double pixelAB = 0;
                double pixelAG = 0;
                double pixelAR = 0; //soma das componentes no interior
                double pixelBB = 0;
                double pixelBG = 0;
                double pixelBR = 0;
                double pixelCB = 0;
                double pixelCG = 0;
                double pixelCR = 0;
                double pixelDB = 0;
                double pixelDG = 0;
                double pixelDR = 0;

                double sumB = 0;
                double sumG = 0;
                double sumR = 0;

                if (nChan == 3)
                {
                    // ------------------------------------------|||---------------------------------------
                    //PROCESSAR O INTERIOR S/ MARGENS

                    //pointer em (0,0)

                    //percorre cada linha de pixeis
                    for (ydest = 0; ydest < (height - 1); ydest++)

                    {
                        //percorre cada pixel de cada linha
                        for (xdest = 0; xdest < (width - 1); xdest++)

                        {

                            dataPtrAux = xdest * nChan + ydest * widthStep + dataPtrOrigem;
                            //Pointer no pixel (xdest, ydest) - A

                            pixelAB = dataPtrAux[0];
                            pixelAG = dataPtrAux[1];
                            pixelAR = dataPtrAux[2];

                            dataPtrAux += nChan;     //Pointer no pixel (xdest+1,ydest) - B

                            pixelBB = dataPtrAux[0];
                            pixelBG = dataPtrAux[1];
                            pixelBR = dataPtrAux[2];

                            dataPtrAux += widthStep - nChan;     //Pointer no pixel (xdest,ydest+1) - C

                            pixelCB = dataPtrAux[0];
                            pixelCG = dataPtrAux[1];
                            pixelCR = dataPtrAux[2];

                            dataPtrAux += nChan;  //Pointer no pixel (xdest+1,ydest+1) - D

                            pixelDB = dataPtrAux[0];
                            pixelDG = dataPtrAux[1];
                            pixelDR = dataPtrAux[2];

                            sumB = Math.Abs(pixelAB - pixelDB) + Math.Abs(pixelBB - pixelCB);
                            sumG = Math.Abs(pixelAG - pixelDG) + Math.Abs(pixelBG - pixelCG);
                            sumR = Math.Abs(pixelAR - pixelDR) + Math.Abs(pixelBR - pixelCR);


                            if (sumB > 255) { sumB = 255; }
                            if (sumG > 255) { sumG = 255; }
                            if (sumR > 255) { sumR = 255; }

                            dataPtr[0] = (byte)(Math.Round(sumB));
                            dataPtr[1] = (byte)(Math.Round(sumG));
                            dataPtr[2] = (byte)(Math.Round(sumR));

                            //termina no pixel (width-1,height-2) CHECK
                            dataPtr += nChan;


                        }

                        dataPtr += nChan + padding;
                        // CONFIRMA-SE QUE ESTAMOS A PERCORRER BEM O INTERIROR
                        //termina no pixel (0,height-1) CHECK

                    }

                    //------------------------------------------||| ---------------------------------------
                    //PROCESSAR OS CANTOS

                    //1º CANTO(0,0) NÃO NECESSITA DE PROCESSAMENTO (ACONTECE NO INTERIOR S/ MARGENS)

                    //2º CANTO(width-1,0) NÃO NECESSITA DE PROCESSAMENTO (ACONTECE NA MARGEM M2)

                    // ------------------------------------------|||---------------------------------------
                    // 3º CANTO(width-1,height-1)

                    dataPtr += (width - 1) * nChan;
                    //Pointer na imagem principal no pixel (width-1,height-1)

                    dataPtr[0] = 0;
                    dataPtr[1] = 0;
                    dataPtr[2] = 0;

                    // ------------------------------------------|||---------------------------------------
                    // 4º CANTO(0,height-1) NÃO NECESSITA DE PROCESSAMENTO (ACONTECE NA MARGEM M3)

                    // ------------------------------------------|||---------------------------------------
                    // FINALMENTE PROCESSAMOS AS MARGENS S/ CANTOS
                    // MARGEM SUPERIOR (M1) NÃO NECESSITA DE PROCESSAMENTO (ACONTECE NO INTERIOR S/MARGENS)

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM LATERAL DIREITA (M2) + 2º CANTO

                    dataPtr -= (height - 1) * widthStep;
                    //Pointer na imagem principal no pixel (width-1,0)

                    //Pointer ao longo da coluna x=width-1
                    for (ydest = 0; ydest < height - 1; ydest++)

                    {
                        dataPtrAux = ((width - 1) * nChan + ydest * widthStep + dataPtrOrigem);
                        //ir ao pixel a ser processado - A

                        pixelAB = dataPtrAux[0];
                        pixelAG = dataPtrAux[1];
                        pixelAR = dataPtrAux[2];

                        dataPtrAux += widthStep;     //Pointer no pixel (1,height-1) - C

                        pixelCB = dataPtrAux[0];
                        pixelCG = dataPtrAux[1];
                        pixelCR = dataPtrAux[2];

                        sumB = 2*Math.Abs(pixelAB - pixelCB);
                        sumG = 2*Math.Abs(pixelAG - pixelCG);
                        sumR = 2*Math.Abs(pixelAR - pixelCR);

                        if (sumB > 255) { sumB = 255; }
                        if (sumG > 255) { sumG = 255; }
                        if (sumR > 255) { sumR = 255; }

                        dataPtr[0] = (byte)(Math.Round(sumB));
                        dataPtr[1] = (byte)(Math.Round(sumG));
                        dataPtr[2] = (byte)(Math.Round(sumR));

                        dataPtr += widthStep;
                        //temina quando dataPtr = (width-1, height-1)
                    }

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM INFERIOR (M3)

                    dataPtr -= (width - 1) * nChan;
                    //Pointer na imagem principal no pixel (1,height-1)

                    //Pointer ao longo da linha y=height-1
                    for (xdest = 0; xdest < width - 1; xdest++)

                    {
                        dataPtrAux = ((xdest) * nChan + (height - 1) * widthStep + dataPtrOrigem);
                        //pixel a ser processado - A

                        pixelAB = dataPtrAux[0];
                        pixelAG = dataPtrAux[1];
                        pixelAR = dataPtrAux[2];

                        dataPtrAux += nChan;     //Pointer no pixel (xdest+1,ydest) - B

                        pixelBB = dataPtrAux[0];
                        pixelBG = dataPtrAux[1];
                        pixelBR = dataPtrAux[2];

                        sumB = 2*Math.Abs(pixelAB - pixelBB);
                        sumG = 2*Math.Abs(pixelAG - pixelBG);
                        sumR = 2*Math.Abs(pixelAR - pixelBR);

                        if (sumB > 255) { sumB = 255; }
                        if (sumG > 255) { sumG = 255; }
                        if (sumR > 255) { sumR = 255; }

                        dataPtr[0] = (byte)(Math.Round(sumB));
                        dataPtr[1] = (byte)(Math.Round(sumG));
                        dataPtr[2] = (byte)(Math.Round(sumR));

                        dataPtr += nChan; //termina quando dataPtr = (width-1 ,height -1) CHECK

                    }

                    // ------------------------------------------|||---------------------------------------
                    // MARGEM LATERAL ESQUERDA (M4) NÃO NECESSITA DE PROCESSAMENTO (ACONTECE NO INTERIOR S/MARGENS)


                }




            }


        }

        public static int[] Histogram_Gray(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;
                
                int[] histogram = new int [256];
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; 
                int padding = m.widthStep - m.nChannels * m.width; 
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    //Array.Clear(histogram, 0, 256);
                    for (y = 0; y < height; y++) //percorre todas as linhas
                    {
                        for (x = 0; x < width; x++) //percorre cada pixel de cada linha
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)(Math.Round(((blue + green + red) / 3.0)));

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            histogram[gray]++;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                    
                }
              
                return histogram;
            }
            

              }

        public static int[] Histogram_RGB(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int[] histogram = new int[256];
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    //Array.Clear(histogram, 0, 256);
                    for (y = 0; y < height; y++) //percorre todas as linhas
                    {
                        for (x = 0; x < width; x++) //percorre cada pixel de cada linha
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)(Math.Round(((blue + green + red) / 3.0)));

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            histogram[gray]++;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }

                }
                return histogram;
            }
        }

        public static int[] Histogram_All(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int[] histogram = new int[256];
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    //Array.Clear(histogram, 0, 256);
                    for (y = 0; y < height; y++) //percorre todas as linhas
                    {
                        for (x = 0; x < width; x++) //percorre cada pixel de cada linha
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)(Math.Round(((blue + green + red) / 3.0)));

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            histogram[gray]++;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }

                }
                return histogram;
            }
        }


    }
}

