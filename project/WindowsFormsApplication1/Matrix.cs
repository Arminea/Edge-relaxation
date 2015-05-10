using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Matrix
    {
    // ================== SOBEL =======================
        public static int[,] Sobel3x3_X
        {
            get
            {
                return new int[,] 
                { { -1,  0,  1, }, 
                  { -2,  0,  2, }, 
                  { -1,  0,  1, }, };
            }
        }

        public static int[,] Sobel3x3_Y
        {
            get
            {
                return new int[,] 
                { {  1,  2,  1, }, 
                  {  0,  0,  0, }, 
                  { -1, -2, -1, }, };
            }
        }

    // ===================== PREWITT ==================

        public static int[,] Prewitt3x3_X
        {
            get
            {
                return new int[,] 
                { { -1,  0,  1, }, 
                  { -1,  0,  1, }, 
                  { -1,  0,  1, }, };
            }
        }

        public static int[,] Prewitt3x3_Y
        {
            get
            {
                return new int[,] 
                { {  1,  1,  1, }, 
                  {  0,  0,  0, }, 
                  { -1, -1, -1, }, };
            }
        }

    // ================== KIRSCH =====================
        public static int[,] Kirsch3x3_X
        {
            get
            {
                return new int[,] 
                { {  5,  5,  5, }, 
                  { -3,  0, -3, }, 
                  { -3, -3, -3, }, };
            }
        }

        public static int[,] Kirsch3x3_Y
        {
            get
            {
                return new int[,] 
                { {  5, -3, -3, }, 
                  {  5,  0, -3, }, 
                  {  5, -3, -3, }, };
            }
        }
    }
}
