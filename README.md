Edge Relaxation - image processing
======================
author: Tereza Štanglová

Edge relaxation is method which could be applied after edge detection. The method stretches cracked edges.

Edge detection
--------------
In my work, edges are detected using gradient operators, like Sobel, Kirsch and Prewitt. Each operator use 3x3 kernel. These kernels are convolved with original image. The results are approximations of the derivatives - in horizontal and vertical changes.

For highlighting of edges could be used a simple threshold. But thresholding may eliminate important edges from image. Alternative for this method is edge relaxation.

Edge relaxation
---------------
Assumption: A weak edge positioned between two strong edges should be strong as well. On the other hans, a strong edge positioned between two weak should be also weak. 
Thus, the existence, non-existence, of edge is based on appearance of its neighborhood.

Edge relaxation is iterative method. Each edge receives a confidence value which is modified in loops. Main goal is convergence of confidences to 0 or 1.

Algorithm:
1. Compute initial confidence of each edge $C_0(e)$ as the normalozed gradient magnitude normalized by the maximum gradient magnitude in the image.
2. *k*=1
3. Compure each edge type based on the confidence of edge neighbors.
4. Modify the confidence of each edge $C_k(e)$ based on its edge type and its previous confidence $C_{k-1}(e)$
5. Test the $C_k(e)$'s to see if they have all converged to either 0 or 1. If so, stop, else increment *k* and go to 3.

This algorithm is taken from the book *Computer Vision* by Dana Ballard and Chris Brown (http://homepages.inf.ed.ac.uk/rbf/BOOKS/BANDB/bandb.htm).

The edge type is concatenation of left and right vertex types. Vertex types are computed from their strength. Vertex type is basically a number of edges emanating from vertex, except edge *e*.

![alt tag](https://cloud.githubusercontent.com/assets/5311408/8393742/e8c1c03c-1d1d-11e5-896c-f4d93d488b87.png)

Examples of vertex types:

![alt tag](https://cloud.githubusercontent.com/assets/5311408/8393768/b07ec188-1d1e-11e5-910a-e3acc7619829.png)

Results
-------
Edge relaxation rapidly improves the initial edge labeling but gives worse result after large number of iterations.

Orignal image | Image after 5 iterations | Image after 30 iterations
------------- | ------------------------ | --------------------------
![alt tag](https://cloud.githubusercontent.com/assets/5311408/8393793/ce02c078-1d1f-11e5-8d97-9a11d2b8475a.PNG) | ![alt tag](https://cloud.githubusercontent.com/assets/5311408/8393795/e35cecf0-1d1f-11e5-938e-ab14c753a7a6.PNG) | ![alt tag](https://cloud.githubusercontent.com/assets/5311408/8393796/ebf3c92e-1d1f-11e5-820a-4bc6c13b631b.PNG)

It does good job with blurred edges and removes noise.

Original image | Image after 1 iteration | Image after 10 iterations
-------------- | ----------------------- | -------------------------
![alt tag](https://cloud.githubusercontent.com/assets/5311408/8393810/859e8ee2-1d20-11e5-8262-06779b8edcc7.PNG) | ![alt tag](https://cloud.githubusercontent.com/assets/5311408/8393813/98eed7a4-1d20-11e5-88e8-719716d1f2f7.PNG) | ![alt tag](https://cloud.githubusercontent.com/assets/5311408/8393811/8d65fbce-1d20-11e5-9768-650ccc29a0db.PNG)
