
/*****************************************************************************
 *                                                                           *
 *  AcuityEngine.cs                                                          *
 *  18 April 2009                                                            *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2009 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Methods for doing general calculations.                                  *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;
using System.Diagnostics;
using MetaphysicsIndustries.Utilities;
using System.Drawing;

namespace MetaphysicsIndustries.Acuity
{
    public partial class AcuityEngine
    {
        public static Matrix LoadImage2(string filename)
        {
            Image fileImage = Image.FromFile(filename);
            if (!(fileImage is Bitmap))
            {
                throw new InvalidOperationException("The file is not in the correct format");
            }

            Bitmap bitmap = (Bitmap)fileImage;
            MemoryImage image = new MemoryImage(bitmap);

            int i;
            int j;

            image.CopyBitmapToPixels();

            Matrix matrix = new Matrix(image.Height, image.Width);

            for (i = 0; i < image.Height; i++)
            {
                for (j = 0; j < image.Width; j++)
                {
                    int argb = image[i, j].ToArgb();

                    matrix[i, j] = (argb & 0x00FFFFFF);
                }
            }

            return matrix;
        }

        public static void SaveImage(string filename, Matrix image)
        {
            MemoryImage mem = new MemoryImage(image.ColumnCount, image.RowCount);
            //mem.Size = new Size(image.ColumnCount, image.RowCount);

            int c;
            int r;

            for (c = 0; c < mem.Width; c++)
            {
                for (r = 0; r < mem.Height; r++)
                {
                    mem[r, c] = Color.FromArgb((int)(((uint)image[r, c]) | 0xFF000000));
                }
            }

            mem.CopyPixelsToBitmap();

            mem.Bitmap.Save(filename);
        }

        public static void SaveImage2(string filename, Matrix image)
        {
            Matrix image2 = image.Clone();
            image2.ApplyToAll(AcuityEngine.ConvertFloatTo24g);
            SaveImage(filename, image2);
        }
    }
}
