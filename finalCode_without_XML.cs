using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MeshInfo
{
    // rename this to vertexList
    public List<Vector3> vertexList = new List<Vector3>();
    // rename this to triangleList
    public List<int> triangleList = new List<int>();
    public List<Color> colorList = new List<Color>();
}
public class NewBehaviourScript1 : MonoBehaviour
{
    /*
    static void WriteFile(String FileName, MeshInfo m) {
        for (int i = 0; i < m.vertexList.Count; i++)
        {
            File.AppendAllText(FileName, m.vertexList[i].ToString() + Environment.NewLine);
        }
        
    }
    static void ReadFile(String FileName, string[] copied)
    {
        //m.vertexList = File.ReadAllText(FileName);
        string[] lines = File.ReadAllLines(FileName);
        copied = lines;
    }
    */
    List<Vector3> verticiesList = new List<Vector3>();
    List<int> triangleVertices = new List<int>();
    /**
        This method takes a list of meshes and combines them into one
      */
    MeshInfo CombineMeshes(List<MeshInfo> meshList)
    {
        MeshInfo combined = new MeshInfo();

        // Combine point list
        for (int i = 0; i < meshList.Count; i++)
        {
            for (int j = 0; j < meshList[i].vertexList.Count; j++)
            {
                combined.vertexList.Add(meshList[i].vertexList[j]);
            }
        }

        // Combine color list
        for (int i = 0; i < meshList.Count; i++)
        {
            for (int j = 0; j < meshList[i].colorList.Count; j++)
            {
                combined.colorList.Add(meshList[i].colorList[j]);
            }
        }
        int sum = 0;
        // Combine face list
        for (int i = 0; i < meshList.Count; i++)
        {

            for (int j = 0; j < meshList[i].triangleList.Count; j++)
            {



                //meshList[i].vertexList.Count * i
                combined.triangleList.Add(meshList[i].triangleList[j] + sum);

            }
            sum += meshList[i].vertexList.Count;
        }

        return combined;
    }
    void PrintMeshInfo(MeshInfo mi)
    {
        print("Vertex List:" + mi.vertexList.Count);
        for (int i = 0; i < mi.vertexList.Count; i++)
        {
            print(mi.vertexList[i]);
        }
        print("Triangle List: " + mi.triangleList.Count);
        for (int i = 0; i < mi.triangleList.Count; i++)
        {
            print(mi.triangleList[i]);
        }
        print("Color List: " + mi.colorList.Count);
        for (int i = 0; i < mi.colorList.Count; i++)
        {
            print(mi.colorList[i]);
        }
    }

    //public Shader shader;
    //public Texture texture;
    //public Color color;

    void Start()
    {


        /* List<MeshInfo> meshList = new List<MeshInfo>();
         for (int i = 0; i < 10; i++) {
             MeshInfo m1 = UnitCube();

             Translate(m1, new Vector3(Random.Range(-15.0f, 15.0f), Random.Range(-15.0f, 15.0f), Random.Range(-15.0f, 15.0f)));
             meshList.Add(m1);
         }
         */

        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo makeBuilding = finalOutput();
        meshList.Add(makeBuilding);





        MeshInfo combinedMesh = CombineMeshes(meshList);


        PrintMeshInfo(combinedMesh);

        /* for (int i = 0; i < combinedMesh.vertexList.Count; i++)
         {

             combinedMesh.colorList.Add(Color.yellow);

         }*/



        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh m = mf.mesh;
        //m.vertices = copied.To;
        m.vertices = combinedMesh.vertexList.ToArray();
        m.triangles = combinedMesh.triangleList.ToArray();
        m.colors = combinedMesh.colorList.ToArray();
        m.RecalculateNormals();




    }
    public MeshInfo fountainArea()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo p1 = cube();
        for (int i = 0; i < p1.vertexList.Count; i++)
        {
            p1.colorList.Add(Color.green);
        }
        scale(p1, new Vector3(3, 0.1f, 3));
        meshList.Add(p1);


        MeshInfo f1 = fountain(5);
        for (int i = 0; i < f1.vertexList.Count; i++)
        {
            f1.colorList.Add(new Color(0.54f, 0, 0));
        }
        Translate(f1, new Vector3(0, 0.3f, 0));
        meshList.Add(f1);



        MeshInfo g1 = grass(20, 120, 400);
        for (int i = 0; i < g1.vertexList.Count; i++)
        {
            g1.colorList.Add(Color.green);
        }
        scale(g1, new Vector3(0.3f, 0.3f, 0.3f));
        Translate(g1, new Vector3(-3, 0.1f, -2));
        meshList.Add(g1);

        MeshInfo g2 = grass(20, 120, 400);
        for (int i = 0; i < g2.vertexList.Count; i++)
        {
            g2.colorList.Add(Color.green);
        }
        scale(g2, new Vector3(0.3f, 0.3f, 0.3f));
        Translate(g2, new Vector3(2.3f, 0.1f, -2));
        meshList.Add(g2);



        MeshInfo combinedMesh = CombineMeshes(meshList);
        // scale(combinedMesh, new Vector3(10, 10, 10));
        return combinedMesh;




    }
    public MeshInfo finalOutput()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo b1 = buildingWithPillar();
        //meshList.Add(b1);

        MeshInfo g1 = ground();
        Translate(g1, new Vector3(0, -1.1f, 0));
        scale(g1, new Vector3(100, 50, 100));
        meshList.Add(g1);
        
        MeshInfo f1 = fountainArea();
    
        scale(f1, new Vector3(10, 10, 10));
        Translate(f1, new Vector3(270, 5, 250));
        meshList.Add(f1);

        MeshInfo f2 = fountainArea();
        Translate(f2, new Vector3(270, 5, -250));
        meshList.Add(f2);

        MeshInfo f3 = fountainArea();
        Translate(f3, new Vector3(-230, 5, 250));
        meshList.Add(f3);

        MeshInfo f4 = fountainArea();
        Translate(f4, new Vector3(-230, 5, -250));
        meshList.Add(f4);


        
        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;
    }
    public MeshInfo ground()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo design = window1(10, 10, 1);


        rotation(design, 90, -90, 0);



        //for (int i = 0; i < design.vertexList.Count; i++)
        //{
        //    design.colorList.Add(Color.red);
        //}
        Translate(design, new Vector3(5, 1.1f, -5));

        for (int i = 0; i < design.vertexList.Count; i++)
        {
            design.colorList[i] = Color.gray;
        }

        meshList.Add(design);

        MeshInfo p1 = cube();
        for (int i = 0; i < p1.vertexList.Count; i++)
        {
            p1.colorList.Add(new Color(0.54f, 0, 0));
        }
        scale(p1, new Vector3(6, 1, 6));
        meshList.Add(p1);

        MeshInfo w1 = cube();
        scale(w1, new Vector3(6f, 1.5f, 0.1f));
        Translate(w1, new Vector3(0, 0.5f, -6));
        for (int i = 0; i < w1.vertexList.Count; i++)
        {
            w1.colorList.Add(Color.gray);
        }

        meshList.Add(w1);

        MeshInfo w2 = cube();
        scale(w2, new Vector3(6f, 1.5f, 0.1f));
        Translate(w2, new Vector3(0, 0.5f, 6));
        for (int i = 0; i < w1.vertexList.Count; i++)
        {
            w2.colorList.Add(Color.gray);
        }

        meshList.Add(w2);



        MeshInfo w3 = cube();
        scale(w3, new Vector3(6f, 1.5f, 0.1f));
        Translate(w3, new Vector3(0, 0.5f, 6));
        for (int i = 0; i < w1.vertexList.Count; i++)
        {
            w3.colorList.Add(Color.gray);
        }
        rotation(w3, 0, 90, 0);
        meshList.Add(w3);

        MeshInfo w4 = cube();
        scale(w4, new Vector3(6f, 1.5f, 0.1f));
        Translate(w4, new Vector3(0, 0.5f, -6));
        for (int i = 0; i < w1.vertexList.Count; i++)
        {
            w4.colorList.Add(Color.gray);
        }
        rotation(w4, 0, 90, 0);
        meshList.Add(w4);

        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;
    }
    public MeshInfo buildingWithPillar()
    {
        List<MeshInfo> meshList = new List<MeshInfo>();
        //done coloring
        MeshInfo m1 = multipleFloors();
        meshList.Add(m1);

        //done coloring
        MeshInfo h1 = HSWallFront(9);
        scale(h1, new Vector3(2, 2.9f, 2));
        Translate(h1, new Vector3(-46, 14, -84));
        meshList.Add(h1);


        //done w
        MeshInfo p1 = minaarLevels(10, 4);
        scale(p1, new Vector3(7, 7, 7));
        Translate(p1, new Vector3(-55, 0, -80));
        meshList.Add(p1);

        MeshInfo p2 = minaarLevels(10, 4);
        scale(p2, new Vector3(7, 7, 7));
        Translate(p2, new Vector3(125, 0, -80));
        meshList.Add(p2);

        MeshInfo p3 = minaarLevels(10, 4);
        scale(p3, new Vector3(7, 7, 7));
        Translate(p3, new Vector3(-55, 0, 80));
        meshList.Add(p3);

        MeshInfo p4 = minaarLevels(10, 4);
        scale(p4, new Vector3(7, 7, 7));
        Translate(p4, new Vector3(125, 0, 80));
        meshList.Add(p4);
        /*float counter = 0;

        for (int i = 0; i < 14; i++) {
            //
           //done coloring
            MeshInfo m = diamondDesign();

            scale(m, new Vector3(5, 5, 5));
            Translate(m,new Vector3(counter-47.5f,121,-80));
            
            meshList.Add(m);
            counter = counter + 12f;

        }*/

        MeshInfo d1 = diamondLoop();
        meshList.Add(d1);

        MeshInfo d2 = diamondLoop();
        Translate(d2, new Vector3(0, 0, 155));
        meshList.Add(d2);

        MeshInfo d3 = diamondLoop2();
        rotation(d3, 0, 90, 0);
        Translate(d3, new Vector3(30, 0, 23f));
        meshList.Add(d3);


        MeshInfo d4 = diamondLoop2();
        rotation(d4, 0, 90, 0);
        Translate(d4, new Vector3(195, 0, 23f));
        meshList.Add(d4);



        MeshInfo topPlane = UnitCube();
        scale(topPlane, new Vector3(85, 1, 80));
        Translate(topPlane, new Vector3(35, 141, -80));
        for (int i = 0; i < topPlane.vertexList.Count; i++)
        {
            topPlane.colorList.Add(new Color(0.54f, 0, 0));
        }

        meshList.Add(topPlane);



        MeshInfo f1 = finalDome();
        scale(f1, new Vector3(2, 2, 2));
        Translate(f1, new Vector3(-35, -140, 0));

        for (int i = 0; i < f1.vertexList.Count; i++)
        {
            f1.colorList.Add(new Color(0, 0.39f, 0));
        }

        meshList.Add(f1);



        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;
    }
    public MeshInfo diamondLoop2()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        float counter = 0;

        for (int i = 0; i < 12; i++)
        {
            //
            //done coloring
            MeshInfo m = diamondDesign();

            scale(m, new Vector3(5, 5, 5));
            Translate(m, new Vector3(counter - 47.5f, 121, -80));

            meshList.Add(m);
            counter = counter + 12f;

        }

        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;

    }
    public MeshInfo diamondLoop()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        float counter = 0;

        for (int i = 0; i < 14; i++)
        {
            //
            //done coloring
            MeshInfo m = diamondDesign();

            scale(m, new Vector3(5, 5, 5));
            Translate(m, new Vector3(counter - 47.5f, 121, -80));

            meshList.Add(m);
            counter = counter + 12f;

        }

        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;

    }
    public MeshInfo finalDome()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo d1 = dome(50);
        scale(d1, new Vector3(4, 4, 4));
        Translate(d1, new Vector3(35, 142, 0));
        meshList.Add(d1);


        MeshInfo g1 = gumbad(20);
        Translate(g1, new Vector3(35, 160, 0));
        meshList.Add(g1);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;
    }
    public MeshInfo multipleFloors()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo f1 = bottomFloor();
        meshList.Add(f1);


        MeshInfo f2 = middleFloor();
        Translate(f2, new Vector3(0, 40, 0));
        meshList.Add(f2);


        MeshInfo f3 = topFloor();
        Translate(f3, new Vector3(0, 80, 0));
        meshList.Add(f3);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;

    }
    public MeshInfo windowWithWall()
    {


        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo windowCenter = window1(20, 20, 2);
        Translate(windowCenter, new Vector3(0, -10, 1));
        meshList.Add(windowCenter);

        MeshInfo windowCenter2 = window1(20, 20, 2);
        Translate(windowCenter2, new Vector3(-20, -10, -1));
        rotation(windowCenter2, 0, 180, 0);
        meshList.Add(windowCenter2);

        MeshInfo rightWall = UnitCube();
        for (int i = 0; i < rightWall.vertexList.Count; i++)
        {
            rightWall.colorList.Add(new Color(0.54f, 0, 0));
        }
        scale(rightWall, new Vector3(15, 20, 1));
        Translate(rightWall, new Vector3(35, 0, 0));

        meshList.Add(rightWall);


        MeshInfo leftWall = UnitCube();
        for (int i = 0; i < leftWall.vertexList.Count; i++)
        {
            leftWall.colorList.Add(new Color(0.54f, 0, 0));
        }
        scale(leftWall, new Vector3(15, 20, 1));
        Translate(leftWall, new Vector3(-15, 0, 0));

        meshList.Add(leftWall);

        MeshInfo upperWall = UnitCube();

        for (int i = 0; i < upperWall.vertexList.Count; i++)
        {
            upperWall.colorList.Add(new Color(0.54f, 0, 0));
        }

        scale(upperWall, new Vector3(10, 5, 1));
        Translate(upperWall, new Vector3(10, 15, 0));

        meshList.Add(upperWall);

        MeshInfo downWall = UnitCube();
        for (int i = 0; i < downWall.vertexList.Count; i++)
        {
            downWall.colorList.Add(new Color(0.54f, 0, 0));
        }

        scale(downWall, new Vector3(10, 5, 1));
        Translate(downWall, new Vector3(10, -15, 0));

        meshList.Add(downWall);



        MeshInfo combinedMesh = CombineMeshes(meshList);



        return combinedMesh;


    }
    public MeshInfo topFloor()
    {
        List<MeshInfo> meshList = new List<MeshInfo>();
        //done
        MeshInfo f1 = floorWithSidePanelQuarter();
        meshList.Add(f1);

        MeshInfo sideWall = UnitCube();
        scale(sideWall, new Vector3(1, 20, 40));
        for (int i = 0; i < sideWall.vertexList.Count; i++)
        {
            sideWall.colorList.Add(new Color(0.54f, 0, 0));
        }
        Translate(sideWall, new Vector3(-50, 20, 0));
        meshList.Add(sideWall);
        //done
        MeshInfo w1 = windowWithWall();
        rotation(w1, 0, 90, 0);
        Translate(w1, new Vector3(-51, 20, -30));
        //scale(w1, new Vector3(2,1,1));
        meshList.Add(w1);

        MeshInfo sideWallRight = UnitCube();
        scale(sideWallRight, new Vector3(1, 20, 40));
        for (int i = 0; i < sideWallRight.vertexList.Count; i++)
        {
            sideWallRight.colorList.Add(new Color(0.54f, 0, 0));
        }

        Translate(sideWallRight, new Vector3(120, 20, 0));
        meshList.Add(sideWallRight);

        MeshInfo w2 = windowWithWall();
        rotation(w2, 0, 90, 0);
        Translate(w2, new Vector3(119, 20, -30));
        //scale(w1, new Vector3(2,1,1));
        meshList.Add(w2);


        MeshInfo backWall = UnitCube();
        scale(backWall, new Vector3(1, 20, 85));
        rotation(backWall, 0, 90, 0);
        for (int i = 0; i < backWall.vertexList.Count; i++)
        {
            backWall.colorList.Add(new Color(0.54f, 0, 0));
        }
        Translate(backWall, new Vector3(-50, 20, 80));
        meshList.Add(backWall);

        MeshInfo roof = UnitCube();
        scale(roof, new Vector3(85, 1, 80));
        for (int i = 0; i < roof.vertexList.Count; i++)
        {
            roof.colorList.Add(new Color(0.54f, 0, 0));
        }
        Translate(roof, new Vector3(35, 40, -80));
        meshList.Add(roof);

        MeshInfo w3 = windowWithWall();
        Translate(w3, new Vector3(6.5f, 20, -81));
        scale(w3, new Vector3(2.13f, 1, 1));
        meshList.Add(w3);
        //MeshInfo topGumbad =


        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;
    }
    public MeshInfo middleFloor()
    {
        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo f1 = floorWithSidePanelQuarter();
        meshList.Add(f1);

        MeshInfo stairs1 = stairsLevels(2, 10, 2, 2, 10);
        rotation(stairs1, 0, -90, 0);
        Translate(stairs1, new Vector3(-30, 0, 50));
        meshList.Add(stairs1);

        MeshInfo sideWall = UnitCube();
        scale(sideWall, new Vector3(1, 20, 40));
        for (int i = 0; i < sideWall.vertexList.Count; i++)
        {
            sideWall.colorList.Add(new Color(0.54f, 0, 0));
        }
        Translate(sideWall, new Vector3(-50, 20, 0));
        meshList.Add(sideWall);

        MeshInfo w1 = windowWithWall();
        rotation(w1, 0, 90, 0);
        Translate(w1, new Vector3(-51, 20, -30));
        //scale(w1, new Vector3(2,1,1));
        meshList.Add(w1);

        MeshInfo sideWallRight = UnitCube();
        scale(sideWallRight, new Vector3(1, 20, 40));
        for (int i = 0; i < sideWallRight.vertexList.Count; i++)
        {
            sideWallRight.colorList.Add(new Color(0.54f, 0, 0));
        }
        Translate(sideWallRight, new Vector3(120, 20, 0));
        meshList.Add(sideWallRight);

        MeshInfo w2 = windowWithWall();
        rotation(w2, 0, 90, 0);
        Translate(w2, new Vector3(119, 20, -30));
        //scale(w1, new Vector3(2,1,1));
        meshList.Add(w2);


        MeshInfo backWall = UnitCube();
        for (int i = 0; i < backWall.vertexList.Count; i++)
        {
            backWall.colorList.Add(new Color(0.54f, 0, 0));
        }

        scale(backWall, new Vector3(1, 20, 85));
        rotation(backWall, 0, 90, 0);
        Translate(backWall, new Vector3(-50, 20, 80));
        meshList.Add(backWall);


        MeshInfo w3 = windowWithWall();
        Translate(w3, new Vector3(6.5f, 20, -81));
        scale(w3, new Vector3(2.13f, 1, 1));
        meshList.Add(w3);

        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;
    }
    public MeshInfo bottomFloor()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo f1 = floorWithSidePanelFull();//done checked
        meshList.Add(f1);

        MeshInfo stairs1 = stairsLevels(2, 10, 2, 2, 10);
        rotation(stairs1, 0, -90, 0);
        Translate(stairs1, new Vector3(-30, 0, 50));
        meshList.Add(stairs1);//checked

        MeshInfo sideWall = UnitCube();
        scale(sideWall, new Vector3(1, 20, 40));
        for (int i = 0; i < sideWall.vertexList.Count; i++)
        {
            sideWall.colorList.Add(new Color(0.54f, 0, 0));
        }

        Translate(sideWall, new Vector3(-50, 20, 0));
        meshList.Add(sideWall);//checked

        MeshInfo w1 = windowWithWall();
        rotation(w1, 0, 90, 0);
        Translate(w1, new Vector3(-51, 20, -30));
        //scale(w1, new Vector3(2,1,1));
        meshList.Add(w1);

        MeshInfo sideWallRight = UnitCube();
        scale(sideWallRight, new Vector3(1, 20, 40));
        for (int i = 0; i < sideWallRight.vertexList.Count; i++)
        {
            sideWallRight.colorList.Add(new Color(0.54f, 0, 0));
        }

        Translate(sideWallRight, new Vector3(120, 20, 0));
        meshList.Add(sideWallRight);

        MeshInfo w2 = windowWithWall();
        rotation(w2, 0, 90, 0);
        Translate(w2, new Vector3(119, 20, -30));
        //scale(w1, new Vector3(2,1,1));
        meshList.Add(w2);


        MeshInfo backWall = UnitCube();
        scale(backWall, new Vector3(1, 20, 85));
        rotation(backWall, 0, 90, 0);
        for (int i = 0; i < backWall.vertexList.Count; i++)
        {
            backWall.colorList.Add(new Color(0.54f, 0, 0));
        }

        Translate(backWall, new Vector3(-50, 20, 80));
        meshList.Add(backWall);



        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;
    }
    public MeshInfo floorWithStairsWithRoof()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo f1 = floorWithSidePanelFull();
        meshList.Add(f1);

        MeshInfo stairs1 = stairsLevels(2, 10, 2, 2, 10);
        rotation(stairs1, 0, -90, 0);
        Translate(stairs1, new Vector3(-30, 0, 50));
        meshList.Add(stairs1);

        MeshInfo f2 = floorWithSidePanelQuarter();
        Translate(f2, new Vector3(0, 40, 0));
        meshList.Add(f2);

        MeshInfo sideWall = UnitCube();
        scale(sideWall, new Vector3(1, 20, 40));
        for (int i = 0; i < sideWall.vertexList.Count; i++)
        {
            sideWall.colorList.Add(new Color(0.95f, 0.64f, 0.37f));
        }

        Translate(sideWall, new Vector3(-50, 20, 0));
        meshList.Add(sideWall);

        MeshInfo w1 = windowWithWall();
        rotation(w1, 0, 90, 0);
        Translate(w1, new Vector3(-51, 20, -30));
        //scale(w1, new Vector3(2,1,1));
        meshList.Add(w1);

        MeshInfo sideWallRight = UnitCube();
        scale(sideWallRight, new Vector3(1, 20, 40));
        for (int i = 0; i < sideWallRight.vertexList.Count; i++)
        {
            sideWallRight.colorList.Add(new Color(0.95f, 0.64f, 0.37f));
        }

        Translate(sideWallRight, new Vector3(120, 20, 0));
        meshList.Add(sideWallRight);

        MeshInfo w2 = windowWithWall();
        rotation(w2, 0, 90, 0);
        Translate(w2, new Vector3(119, 20, -30));
        //scale(w1, new Vector3(2,1,1));
        meshList.Add(w2);


        MeshInfo backWall = UnitCube();
        scale(backWall, new Vector3(1, 20, 85));
        rotation(backWall, 0, 90, 0);
        for (int i = 0; i < backWall.vertexList.Count; i++)
        {
            backWall.colorList.Add(new Color(0.95f, 0.64f, 0.37f));
        }
        Translate(backWall, new Vector3(-50, 20, 80));
        meshList.Add(backWall);



        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;
    }
    public MeshInfo floorWithSidePanelQuarter()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo f1 = floor2();
        meshList.Add(f1);


        MeshInfo sidePanel = UnitCube();

        for (int i = 0; i < sidePanel.vertexList.Count; i++)
        {
            sidePanel.colorList.Add(Color.gray);
        }

        Translate(sidePanel, new Vector3(-9, 0, -1.30f));
        scale(sidePanel, new Vector3(5, 1, 62));
        meshList.Add(sidePanel);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;


    }
    public MeshInfo floorWithSidePanelFull()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo f1 = floor();
        meshList.Add(f1);


        MeshInfo sidePanel = UnitCube();

        for (int i = 0; i < sidePanel.vertexList.Count; i++)
        {
            sidePanel.colorList.Add(Color.gray);
        }
        Translate(sidePanel, new Vector3(-9, 0, -1));
        scale(sidePanel, new Vector3(5, 1, 80));
        meshList.Add(sidePanel);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;


    }
    public MeshInfo floor2()
    {


        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo s1 = sector();
        meshList.Add(s1);

        MeshInfo s2 = simpleSector();
        Translate(s2, new Vector3(80, 0, 0));
        meshList.Add(s2);

        MeshInfo s3 = sector1();
        Translate(s3, new Vector3(0, 0, -40));
        meshList.Add(s3);

        MeshInfo s4 = sector();
        Translate(s4, new Vector3(80, 0, -80));
        meshList.Add(s4);

        /*MeshInfo stairs1 = stairsLevels(2,10,2,2,10);
        rotation(stairs1, 0, -90, 0);
        Translate(stairs1, new Vector3(-30, 1, 50));
        meshList.Add(stairs1);

        */


        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;


    }
    public MeshInfo floor()
    {


        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo s1 = simpleSector();
        meshList.Add(s1);

        MeshInfo s2 = sector();
        Translate(s2, new Vector3(80, 0, 0));
        meshList.Add(s2);

        MeshInfo s3 = sector1();
        Translate(s3, new Vector3(0, 0, -40));
        meshList.Add(s3);

        MeshInfo s4 = sector();
        Translate(s4, new Vector3(80, 0, -80));
        meshList.Add(s4);

        /*MeshInfo stairs1 = stairsLevels(2,10,2,2,10);
        rotation(stairs1, 0, -90, 0);
        Translate(stairs1, new Vector3(-30, 1, 50));
        meshList.Add(stairs1);

        */


        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;


    }
    public MeshInfo simpleSector()
    {
        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo plane1 = UnitCube();
        scale(plane1, new Vector3(40, 1, 40));

        for (int i = 0; i < plane1.vertexList.Count; i++)
        {
            plane1.colorList.Add(Color.gray);
        }

        meshList.Add(plane1);


        MeshInfo combinedMesh = CombineMeshes(meshList);
        return combinedMesh;
    }
    public MeshInfo sector()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo plane1 = UnitCube();
        scale(plane1, new Vector3(40, 1, 40));

        for (int i = 0; i < plane1.vertexList.Count; i++)
        {
            plane1.colorList.Add(Color.gray);
        }

        meshList.Add(plane1);

        MeshInfo table1 = table(7, 0.2f, 4, 0.5f, 7, 20);
        Translate(table1, new Vector3(0, 7, 0));

        meshList.Add(table1);

        MeshInfo chair1 = chair(4, 0.2f, 4, 0.5f, 5, 20);
        Translate(chair1, new Vector3(0, 5, 12));
        meshList.Add(chair1);

        MeshInfo grass1 = grass(80, 80, 200);
        Translate(grass1, new Vector3(-15, 1, 10));
        //meshList.Add(grass1);

        MeshInfo flower1 = flower(1);
        scale(flower1, new Vector3(0.1f, 0.1f, 0.1f));
        Translate(flower1, new Vector3(-15, 4, 10));
        //meshList.Add(flower1);


        MeshInfo table2 = table(7, 0.2f, 4, 0.5f, 7, 20);
        rotation(table2, 0, 90, 0);
        Translate(table2, new Vector3(-10, 7, 40));
        meshList.Add(table2);

        MeshInfo chair2 = chair(4, 0.2f, 4, 0.5f, 5, 20);
        rotation(chair2, 0, 90, 0);
        Translate(chair2, new Vector3(0, 5, 50));
        meshList.Add(chair2);

        MeshInfo combinedMesh = CombineMeshes(meshList);
        return combinedMesh;
    }
    public MeshInfo fountain(int levels)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        float radius = 0.7f;
        int count = 0;
        float scaleUnit = 1;

        for (int i = 0; i < levels; i++)
        {

            if (count % 2 == 0)
            {

                MeshInfo f = dount(radius, 0.1f, 6, 6);
                scale(f, new Vector3(1, scaleUnit, 1));
                meshList.Add(f);
            }
            else
            {

                MeshInfo f = dount(radius, 0.1f, 6, 6);
                rotation(f, 0, 30, 0);
                scale(f, new Vector3(1, scaleUnit, 1));
                meshList.Add(f);
            }
            radius = radius + 0.3f;
            count++;
            scaleUnit = scaleUnit + 0.5f;
        }



        MeshInfo combinedMesh = CombineMeshes(meshList);


        return combinedMesh;



    }
    public MeshInfo dount(float dountRadius, float dountThickness, int sides, int dountLayers)
    {

        MeshInfo Doughnut = new MeshInfo();



        float dountSegmentSize = 2 * Mathf.PI / (float)sides;//
        float dountLayerSize = 2 * Mathf.PI / (float)dountLayers;

        float x, y, z;

        for (int i = 0; i < sides; i++)
        {
            // Find first or next segment offset of side first side second side and so on
            int n = (i + 1) % sides;

            // Find the current and comming segments layer for every side
            int currentdountLayereOffset = i * dountLayers;

            int nextdountLayereOffset = n * dountLayers;

            for (int j = 0; j < dountLayers; j++)
            {
                // Find first or next vertex offset layer first layer second layer and so on
                int m = (j + 1) % dountLayers;


                //vertices for triangles
                int vert1 = currentdountLayereOffset + j;
                int vert2 = currentdountLayereOffset + m;

                int vert3 = nextdountLayereOffset + m;
                int vert4 = nextdountLayereOffset + j;


                x = (dountRadius + dountThickness * Mathf.Cos(j * dountLayerSize)) * Mathf.Cos(i * dountSegmentSize);

                y = dountThickness * Mathf.Sin(j * dountLayerSize);

                z = (dountRadius + dountThickness * Mathf.Cos(j * dountLayerSize)) * Mathf.Sin(i * dountSegmentSize);

                Doughnut.vertexList.Add(new Vector3(x, y, z));

                Doughnut.triangleList.Add(vert1);
                Doughnut.triangleList.Add(vert2);
                Doughnut.triangleList.Add(vert3);

                Doughnut.triangleList.Add(vert3);
                Doughnut.triangleList.Add(vert4);
                Doughnut.triangleList.Add(vert1);

            }//end of inner or second loop

        }//end of first loop
        print("count: " + Doughnut.vertexList.Count);

        print("triangles: " + Doughnut.triangleList.Count);
        return Doughnut;
    }
    public MeshInfo grass(float width, float length, int numberOfBlades)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();
        for (int i = 0; i < numberOfBlades; i++)
        {
            MeshInfo m1 = grassBlade(UnityEngine.Random.Range(0, 3));

            rotation(m1, 0, UnityEngine.Random.Range(10, 180), 0);
            Translate(m1, new Vector3(UnityEngine.Random.Range(0, width) / 10, 0, UnityEngine.Random.Range(0, length) / 10));
            meshList.Add(m1);
        }


        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;

    }
    public MeshInfo grassBlade(int type)
    {

        MeshInfo m = new MeshInfo();

        if (type == 1)//straight
        {
            m.vertexList.Add(new Vector3(0, 0, 0));//0

            m.vertexList.Add(new Vector3(0.05f, UnityEngine.Random.Range(5, 10) / 10f, 0));//1

            m.vertexList.Add(new Vector3(0.1f, 0, 0));//2

            m.vertexList.Add(new Vector3(0, 0, 0));//3

            m.vertexList.Add(new Vector3(0.05f, UnityEngine.Random.Range(5, 10) / 10f, 0));

            m.vertexList.Add(new Vector3(0.1f, 0, 0));

            m.triangleList.Add(0);

            m.triangleList.Add(1);

            m.triangleList.Add(2);

            m.triangleList.Add(5);

            m.triangleList.Add(4);

            m.triangleList.Add(3);
        }
        else
        {
            m.vertexList.Add(new Vector3(0, 0, 0));//0

            m.vertexList.Add(new Vector3(0.2f, UnityEngine.Random.Range(5, 10) / 10f, 0));

            m.vertexList.Add(new Vector3(0.05f, 0, 0));

            m.vertexList.Add(new Vector3(0, 0, 0));//3

            m.vertexList.Add(new Vector3(0.2f, UnityEngine.Random.Range(5, 10) / 10f, 0));

            m.vertexList.Add(new Vector3(0.05f, 0, 0));

            m.triangleList.Add(0);

            m.triangleList.Add(1);

            m.triangleList.Add(2);

            m.triangleList.Add(5);

            m.triangleList.Add(4);

            m.triangleList.Add(3);
        }

        /* m.triangleList.Add(2);

         m.triangleList.Add(1);

         m.triangleList.Add(0);
         */
        return m;
    }
    public MeshInfo minaarLevels(float height, float level)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();
        float height2 = 0;
        float scaling = 1;

        for (int i = 0; i < level; i++)
        {
            MeshInfo m1 = minaar(height);
            Translate(m1, new Vector3(0, height2, 0));
            scale(m1, new Vector3(scaling, 1, scaling));
            meshList.Add(m1);
            height2 += 6;
            scaling -= 0.2f;
        }



        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 0; i < combinedMesh.vertexList.Count; i++)
        {
            combinedMesh.colorList.Add(new Color(0.54f, 0, 0));
        }

        return combinedMesh;
    }
    public MeshInfo minaar(float height)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo segment1 = unitCylinderSects(6, height, 1);
        meshList.Add(segment1);


        MeshInfo segment2 = unitCylinderSects(6, 1, 2);
        Translate(segment2, new Vector3(0, 5, 0));
        meshList.Add(segment2);









        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;


    }
    public MeshInfo topDesign(float width, float height, float length)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        float move = 0;


        MeshInfo c1 = UnitCube();

        scale(c1, new Vector3(width, height, length));
        Translate(c1, new Vector3(0, 0, 0));
        meshList.Add(c1);

        MeshInfo c2 = UnitCube();

        scale(c2, new Vector3(width, 0.5f, length));
        Translate(c2, new Vector3(0, 5, 0));
        meshList.Add(c2);




        for (int i = 0; i < width / 2; i++)
        {
            MeshInfo d1 = diamondDesign();
            // scale(d1, new Vector3(, 0.5f, 0.5f));
            Translate(d1, new Vector3(-move + c1.vertexList[2].x + 45, height, 0));
            meshList.Add(d1);
            move = move + 2.4f;
        }


        MeshInfo combinedMesh = CombineMeshes(meshList);


        return combinedMesh;


    }
    public MeshInfo gumbad2(int radius)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo g1 = gumbad(10);
        scale(g1, new Vector3(0.5f, 0.5f, 0.5f));
        Translate(g1, new Vector3(0, 5, 0));
        meshList.Add(g1);


        MeshInfo d1 = dome(10);
        meshList.Add(d1);

        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;


    }
    public MeshInfo dome1(int radius)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo c1 = circleUpdated(20);
        Translate(c1, new Vector3(0, radius, 0));
        meshList.Add(c1);
        double radiusChange = 0;
        float radiusFloat = 0;

        for (int i = 5; i > 1; i--)
        {

            radiusChange = Math.Sqrt(36 - ((i - 1) * (i - 1)));
            radiusFloat = (float)radiusChange;

            MeshInfo c2 = circleOnlyVert(20, radiusFloat);
            Translate(c2, new Vector3(0, i, 0));
            meshList.Add(c2);

        }

        MeshInfo center = circleOnlyVert(20, radius);
        meshList.Add(center);


        MeshInfo down = unitCircleDownwards(radius);
        meshList.Add(down);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 1; i < 20; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
        }

        combinedMesh.triangleList.Add(20);
        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(20);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(1);


        for (int i = 21; i < 40; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(41);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(41);


        for (int i = 41; i < 60; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(61);
        combinedMesh.triangleList.Add(41);
        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(61);


        for (int i = 61; i < 80; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(81);
        combinedMesh.triangleList.Add(61);
        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(81);

        for (int i = 81; i < 100; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(101);
        combinedMesh.triangleList.Add(81);
        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(120);
        combinedMesh.triangleList.Add(101);

        return combinedMesh;

    }
    public MeshInfo gumbad(int radius)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();



        MeshInfo d1 = dome1(radius);
        meshList.Add(d1);


        MeshInfo c1 = UnitCone();
        scale(c1, new Vector3(1, radius, 1));
        Translate(c1, new Vector3(0, radius, 0));
        meshList.Add(c1);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;


    }
    public MeshInfo diamondDesign()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo base1 = prism();
        scale(base1, new Vector3(2, 2, 2));
        meshList.Add(base1);


        MeshInfo diamond = UnitCube();

        rotation(diamond, 0, 0, 45);
        scale(diamond, new Vector3(1, 1, 0.5f));
        Translate(diamond, new Vector3(1, 2.4f, 0));
        meshList.Add(diamond);



        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 0; i < combinedMesh.vertexList.Count; i++)
        {
            combinedMesh.colorList.Add(new Color(0.54f, 0, 0));
        }

        return combinedMesh;

    }
    public MeshInfo prism()
    {

        MeshInfo m = new MeshInfo();


        m.vertexList.Add(new Vector3(0, 0));//0
        m.vertexList.Add(new Vector3(0.5f, 0.5f));//1
        m.vertexList.Add(new Vector3(1, 0));//2

        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);

        m.vertexList.Add(new Vector3(0, 0, 0.5f));//3
        m.vertexList.Add(new Vector3(0.5f, 0.5f, 0.5f));//4
        m.vertexList.Add(new Vector3(1, 0, 0.5f));//5

        m.triangleList.Add(5);
        m.triangleList.Add(4);
        m.triangleList.Add(3);

        m.triangleList.Add(0);
        m.triangleList.Add(3);
        m.triangleList.Add(1);

        m.triangleList.Add(3);
        m.triangleList.Add(4);
        m.triangleList.Add(1);


        m.triangleList.Add(2);
        m.triangleList.Add(1);
        m.triangleList.Add(4);

        m.triangleList.Add(2);
        m.triangleList.Add(4);
        m.triangleList.Add(5);



        return m;

    }
    public MeshInfo mazaar(int doors)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo h1 = HSWallFourSidedPillar(doors);

        meshList.Add(h1);


        MeshInfo d1 = gumbad2(doors);
        scale(d1, new Vector3(doors / 2, doors / 2, doors / 2));
        Translate(d1, new Vector3(doors * 4, 15.5f, -doors * 5));


        meshList.Add(d1);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;
    }
    public MeshInfo HSWallFourSidedPillar(int doors)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo bottom = UnitCube();
        scale(bottom, new Vector3(doors * 6f, 1, doors * 5f));
        Translate(bottom, new Vector3(doors * 3.7f, -6, doors * -10 + (doors / 1.7f)));
        meshList.Add(bottom);

        /*MeshInfo roof = UnitCube();
        scale(roof, new Vector3(doors * 6f, 1, doors * 5f));
        Translate(roof, new Vector3(doors * 3.7f, 10, doors * -10 + (doors / 1.7f)));
        meshList.Add(roof);
        */
        MeshInfo top = topDesign(doors * 6f, 1, doors * 5f);
        Translate(top, new Vector3(doors * 3.7f, 10, doors * -10 + (doors / 1.7f)));
        meshList.Add(top);

        MeshInfo front = HSWallFront(doors - 1);
        meshList.Add(front);

        MeshInfo left = HSWallFront(doors - 1);
        rotation(left, 0, 90, 0);
        Translate(left, new Vector3(9 * doors, 0, -5));
        meshList.Add(left);

        MeshInfo right = HSWallFront(doors - 1);
        rotation(right, 0, 90, 0);
        Translate(right, new Vector3(-10, 0, -5));
        meshList.Add(right);





        MeshInfo back = HSWallFront(doors - 1);
        Translate(back, new Vector3(0, 0, -9 * doors - 2));
        meshList.Add(back);




        MeshInfo pillar1 = minaarLevels(5, doors - 2);
        scale(pillar1, new Vector3(3, 3, 3));
        Translate(pillar1, new Vector3(-doors, -5, 1));
        meshList.Add(pillar1);

        MeshInfo pillar2 = minaarLevels(5, doors - 2);
        scale(pillar2, new Vector3(3, 3, 3));
        Translate(pillar2, new Vector3(-doors - 2, -5, -doors * 9 - 2));
        meshList.Add(pillar2);

        MeshInfo pillar3 = minaarLevels(5, doors - 2);
        scale(pillar3, new Vector3(3, 3, 3));
        Translate(pillar3, new Vector3(doors * 9 - 2, -5, -doors * 9 - 2));
        meshList.Add(pillar3);

        MeshInfo pillar4 = minaarLevels(5, doors - 2);
        scale(pillar4, new Vector3(3, 3, 3)); Translate(pillar4, new Vector3(doors * 9 - 2, -5, 1));
        meshList.Add(pillar4);

        MeshInfo combinedMesh = CombineMeshes(meshList);


        return combinedMesh;


    }
    public MeshInfo HSWallFourSided(int doors)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo bottom = UnitCube();
        scale(bottom, new Vector3(doors * 4.9f, 1, doors * 4.9f));
        Translate(bottom, new Vector3(doors * 5 - (doors / 2), -6, doors * -10 + (doors / 1.7f)));
        meshList.Add(bottom);

        MeshInfo roof = UnitCube();
        scale(roof, new Vector3(doors * 4.9f, 1, doors * 4.9f));
        Translate(roof, new Vector3(doors * 5 - (doors / 2), 10, doors * -10 + (doors / 1.7f)));
        meshList.Add(roof);



        MeshInfo front = HSWallFront(doors);
        meshList.Add(front);

        MeshInfo left = HSWallFront(doors);
        rotation(left, 0, 90, 0);
        Translate(left, new Vector3(9 * doors, 0, 0));
        meshList.Add(left);

        MeshInfo right = HSWallFront(doors);
        rotation(right, 0, 90, 0);
        Translate(right, new Vector3(-2, 0, 0));
        meshList.Add(right);





        MeshInfo back = HSWallFront(doors);
        Translate(back, new Vector3(0, 0, -9 * doors - 2));
        meshList.Add(back);

        MeshInfo combinedMesh = CombineMeshes(meshList);


        return combinedMesh;


    }
    public MeshInfo HSWallFront(int numberOfHS)
    {
        List<MeshInfo> meshList = new List<MeshInfo>();


        float gap = 0;




        for (int i = 0; i < numberOfHS; i++)
        {


            MeshInfo h = horseShoeHalfPillar();

            Translate(h, new Vector3(gap, 0, 0));

            meshList.Add(h);

            gap = gap + 9;

        }

        MeshInfo rightSidePillar = UnitCube();
        scale(rightSidePillar, new Vector3(1, 7, 1));
        Translate(rightSidePillar, new Vector3(-1, 2, 0));

        meshList.Add(rightSidePillar);




        MeshInfo leftSidePillar = UnitCube();
        scale(leftSidePillar, new Vector3(1, 7, 1));
        Translate(leftSidePillar, new Vector3(gap + 1, 2, 0));

        meshList.Add(leftSidePillar);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 0; i < combinedMesh.vertexList.Count; i++)
        {
            combinedMesh.colorList.Add(new Color(0.54f, 0, 0));
        }

        return combinedMesh;


    }
    public MeshInfo horseShoeHalfPillar()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();



        MeshInfo h1 = horseShoeHalf();

        Translate(h1, new Vector3(0, 5, 0));
        meshList.Add(h1);



        MeshInfo p1 = UnitCube();

        scale(p1, new Vector3(1.5f, 5, 1));
        Translate(p1, new Vector3(4.5f, 0, 0));
        meshList.Add(p1);






        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;




    }
    public MeshInfo horseShoeHalf()
    {



        MeshInfo m = new MeshInfo();
        //front up

        #region front

        float fx = 0;
        for (int i = 0; i < 5; i++)
        {
            m.vertexList.Add(new Vector3(fx, 4, 0));//0 to 4 total 5 vertexpoints
            fx++;
        }

        //center
        //outerright for innner left and outerleft for inner right

        float fy = 3;
        for (int i = 0; i < 4; i++)
        {
            m.vertexList.Add(new Vector3(4, fy, 0));//5 6 7 8 
            fy--;
        }





        float x;
        float iy;
        float angle = 90;
        float xangle = 90;
        //  m.vertexList.Add(new Vector3(-1,0,0));
        for (int i = 0; i < 10; i++)
        {
            // m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), 0, (Mathf.Sin((z * Mathf.PI) / 180))));




            x = 3 * (Mathf.Cos((xangle * Mathf.PI) / 180));
            iy = 3 * (Mathf.Sin((angle * Mathf.PI) / 180));

            m.vertexList.Add(new Vector3(x, iy, 0));// 9 10 11 12 13 14 15 16 17 18 
            angle += 10;
            xangle -= 10;

            //   print(i+"inner : "+m.vertexList[i]);
        }



        //right
        float frx = 5;
        for (int i = 0; i < 4; i++)
        {
            m.vertexList.Add(new Vector3(frx, 4, 0));//19 20 21 22 
            frx++;
        }




        //second quater circle

        float sx;
        float siy;
        float sangle = 0;
        float sxangle = 180;
        //  m.vertexList.Add(new Vector3(-1,0,0));
        for (int i = 0; i < 10; i++)
        {
            // m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), 0, (Mathf.Sin((z * Mathf.PI) / 180))));




            sx = 3 * (3 + (Mathf.Cos((sxangle * Mathf.PI) / 180)));
            siy = 3 * (Mathf.Sin((sangle * Mathf.PI) / 180));

            m.vertexList.Add(new Vector3(sx, siy, 0));//23 24 25 26 27 28 29 30 31 32 
            sangle += 10;
            sxangle -= 10;

            print(i + "inner : " + m.vertexList[i]);
        }




        m.vertexList.Add(new Vector3(9, 4, 0));






        for (int i = 0; i < 8; i++)
        {
            m.triangleList.Add(10 + i);
            m.triangleList.Add(0 + i);
            m.triangleList.Add(11 + i);


            m.triangleList.Add(0 + i);
            m.triangleList.Add(1 + i);
            m.triangleList.Add(11 + i);
        }
        m.triangleList.Add(9);
        m.triangleList.Add(0);
        m.triangleList.Add(10);

        //

        int j = 0;
        for (int i = 8; i > 4; i--)
        {


            m.triangleList.Add(23 + j);
            m.triangleList.Add(i);
            m.triangleList.Add(24 + j);

            m.triangleList.Add(i);
            m.triangleList.Add(i - 1);
            m.triangleList.Add(24 + j);

            j++;

        }
        // print("value of j: " + j);


        int k = 0;
        for (int jj = 19; jj < 23; jj++)
        {
            m.triangleList.Add(27 + k);
            m.triangleList.Add(jj);
            m.triangleList.Add(28 + k);
            k++;


        }

        m.triangleList.Add(31);
        m.triangleList.Add(22);
        m.triangleList.Add(32);

        m.triangleList.Add(22);
        m.triangleList.Add(33);
        m.triangleList.Add(32);


        m.triangleList.Add(4);
        m.triangleList.Add(19);
        m.triangleList.Add(27);

        m.triangleList.Add(19);
        m.triangleList.Add(20);
        m.triangleList.Add(28);

        m.triangleList.Add(20);
        m.triangleList.Add(21);
        m.triangleList.Add(29);

        m.triangleList.Add(21);
        m.triangleList.Add(22);
        m.triangleList.Add(30);


        //



        //print("total number of points for horsehoe: " + m.vertexList.Count);

        #endregion rent

        #region back



        float bfx = 0;
        for (int i = 0; i < 5; i++)
        {
            m.vertexList.Add(new Vector3(bfx, 4, 2));//0 to 4 total 5 vertexpoints
            bfx++;
        }

        //center
        //outerright for innner left and outerleft for inner right

        float bfy = 3;
        for (int i = 0; i < 4; i++)
        {
            m.vertexList.Add(new Vector3(4, bfy, 2));//5 6 7 8 
            bfy--;
        }





        float bx;
        float biy;
        float bangle = 90;
        float bxangle = 90;
        //  m.vertexList.Add(new Vector3(-1,0,0));
        for (int i = 0; i < 10; i++)
        {
            // m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), 0, (Mathf.Sin((z * Mathf.PI) / 180))));




            bx = 3 * (Mathf.Cos((bxangle * Mathf.PI) / 180));
            biy = 3 * (Mathf.Sin((bangle * Mathf.PI) / 180));

            m.vertexList.Add(new Vector3(bx, biy, 2));// 9 10 11 12 13 14 15 16 17 18 
            bangle += 10;
            bxangle -= 10;

            //   print(i+"inner : "+m.vertexList[i]);
        }



        //right
        float bfrx = 5;
        for (int i = 0; i < 4; i++)
        {
            m.vertexList.Add(new Vector3(bfrx, 4, 2));//19 20 21 22 
            bfrx++;
        }




        //second quater circle

        float bsx;
        float bsiy;
        float bsangle = 0;
        float bsxangle = 180;
        //  m.vertexList.Add(new Vector3(-1,0,0));
        for (int i = 0; i < 10; i++)
        {
            // m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), 0, (Mathf.Sin((z * Mathf.PI) / 180))));




            bsx = 3 * (3 + (Mathf.Cos((bsxangle * Mathf.PI) / 180)));
            bsiy = 3 * (Mathf.Sin((bsangle * Mathf.PI) / 180));

            m.vertexList.Add(new Vector3(bsx, bsiy, 2));//23 24 25 26 27 28 29 30 31 32 
            bsangle += 10;
            bsxangle -= 10;

            // print(i + "inner : " + m.vertexList[i]);
        }




        m.vertexList.Add(new Vector3(9, 4, 2));






        for (int i = 0; i < 8; i++)
        {
            m.triangleList.Add(10 + i + 34);
            m.triangleList.Add(11 + i + 34);
            m.triangleList.Add(0 + i + 34);

            m.triangleList.Add(0 + i + 34);
            m.triangleList.Add(11 + i + 34);
            m.triangleList.Add(1 + i + 34);
        }
        m.triangleList.Add(9 + 34);
        m.triangleList.Add(10 + 34);
        m.triangleList.Add(0 + 34);
        //

        int bj = 0;
        for (int i = 8; i > 4; i--)
        {


            m.triangleList.Add(23 + bj + 34);
            m.triangleList.Add(24 + bj + 34);
            m.triangleList.Add(i + 34);


            m.triangleList.Add(i + 34);
            m.triangleList.Add(24 + bj + 34);
            m.triangleList.Add(i - 1 + 34);
            bj++;

        }
        // print("value of j: " + j);


        int bk = 0;
        for (int jj = 19; jj < 23; jj++)
        {
            m.triangleList.Add(27 + bk + 34);

            m.triangleList.Add(28 + bk + 34);
            m.triangleList.Add(jj + 34);
            bk++;


        }

        m.triangleList.Add(31 + 34);

        m.triangleList.Add(32 + 34);
        m.triangleList.Add(22 + 34);


        m.triangleList.Add(22 + 34);

        m.triangleList.Add(32 + 34);
        m.triangleList.Add(33 + 34);



        m.triangleList.Add(4 + 34);

        m.triangleList.Add(27 + 34);
        m.triangleList.Add(19 + 34);



        m.triangleList.Add(19 + 34);

        m.triangleList.Add(28 + 34);
        m.triangleList.Add(20 + 34);


        m.triangleList.Add(20 + 34);

        m.triangleList.Add(29 + 34);
        m.triangleList.Add(21 + 34);


        m.triangleList.Add(21 + 34);

        m.triangleList.Add(30 + 34);
        m.triangleList.Add(22 + 34);

        //upertri
        m.triangleList.Add(0);
        m.triangleList.Add(34);
        m.triangleList.Add(33);


        m.triangleList.Add(33 + 34);
        m.triangleList.Add(33);
        m.triangleList.Add(34);


        for (int i = 9; i < 18; i++)
        {
            m.triangleList.Add(i);
            m.triangleList.Add(i + 1);
            m.triangleList.Add(i + 34);

            m.triangleList.Add(i + 34);
            m.triangleList.Add(1 + i);
            m.triangleList.Add(35 + i);


        }

        for (int i = 23; i < 32; i++)
        {
            m.triangleList.Add(i);
            m.triangleList.Add(i + 1);
            m.triangleList.Add(i + 34);

            m.triangleList.Add(i + 34);
            m.triangleList.Add(1 + i);
            m.triangleList.Add(35 + i);
        }



        #endregion back




        return m;
    }
    public MeshInfo horseShoePillar()
    {



        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo h1 = horseShoe();
        Translate(h1, new Vector3(0, 5, 0));
        meshList.Add(h1);

        MeshInfo p1 = UnitCube();

        scale(p1, new Vector3(0.5f, 5, 1));
        Translate(p1, new Vector3(-3.5f, 0, -2));
        meshList.Add(p1);


        MeshInfo p2 = UnitCube();

        scale(p2, new Vector3(0.5f, 5, 1));
        Translate(p2, new Vector3(3.5f, 0, -2));
        meshList.Add(p2);

        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;

    }
    public MeshInfo horseShoe()
    {



        MeshInfo m = new MeshInfo();












        float lx = -4f;
        float y = 0;
        for (int i = 0; i < 5; i++)
        {

            m.vertexList.Add(new Vector3(lx, y, 0));
            y += 1;
            //  print("outer left: "+m.vertexList[i]);

        }

        //upper

        for (int i = 0; i < 8; i++)
        {
            lx += 1;
            m.vertexList.Add(new Vector3(lx, 4f, 0));
            // print("outter upper: "+m.vertexList[i]);
        }

        //right


        for (int i = 0; i < 5; i++)
        {

            y -= 1;
            m.vertexList.Add(new Vector3(4f, y, 0));



            //  print("right upper:  " + m.vertexList[i]);


        }

        //  print(m.vertexList.Count);




        //start 2nd side



        float slx = -4f;
        float sy = 0;
        for (int i = 0; i < 5; i++)
        {

            m.vertexList.Add(new Vector3(slx, sy, -2));
            sy += 1f;
            //   print("second left: "+m.vertexList[i]);

        }

        //upper

        for (int i = 0; i < 8; i++)
        {
            slx += 1;
            m.vertexList.Add(new Vector3(slx, 4f, -2));
            //print("second upper: "+m.vertexList[i]);
        }

        //right


        for (int i = 0; i < 5; i++)
        {

            sy -= 1;
            m.vertexList.Add(new Vector3(4f, sy, -2));



            //  print("second right:  "+m.vertexList[i]);

        }


        // print("total vertices of outter bothside: " + m.vertexList.Count);

        //  int x = 630;
        // int z = 0;
        //   m.vertexList.Add(new Vector3(0, 0, 0));
        float x;
        float iy;
        float angle = 0;
        float xangle = 180;
        //  m.vertexList.Add(new Vector3(-1,0,0));
        for (int i = 0; i < 19; i++)
        {
            // m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), 0, (Mathf.Sin((z * Mathf.PI) / 180))));




            x = 3 * (Mathf.Cos((xangle * Mathf.PI) / 180));
            iy = 3 * (Mathf.Sin((angle * Mathf.PI) / 180));

            m.vertexList.Add(new Vector3(x, iy, 0));
            angle += 10;
            xangle -= 10;

            //   print(i+"inner : "+m.vertexList[i]);
        }







        float sx;
        float siy;
        float sangle = 0;
        float sxangle = 180;
        for (int i = 0; i < 19; i++)
        {
            // m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), 0, (Mathf.Sin((z * Mathf.PI) / 180))));



            sx = 3 * (Mathf.Cos((sxangle * Mathf.PI) / 180));
            siy = 3 * (Mathf.Sin((sangle * Mathf.PI) / 180));

            m.vertexList.Add(new Vector3(sx, siy, -2));
            sangle += 10;
            sxangle -= 10;

            print(i + "inner second : " + m.vertexList[i]);
        }



        //end 2nd side 



        #region triangleforhorseshoe



        for (int i = 0; i < 17; i++)
        {
            m.triangleList.Add(36 + i);
            m.triangleList.Add(1 + i);
            m.triangleList.Add(0 + i);

            m.triangleList.Add(36 + i);
            m.triangleList.Add(37 + i);
            m.triangleList.Add(1 + i);


            m.triangleList.Add(55 + i);
            m.triangleList.Add(18 + i);
            m.triangleList.Add(19 + i);


            m.triangleList.Add(55 + i);
            m.triangleList.Add(19 + i);
            m.triangleList.Add(56 + i);


        }
        //for finishing 
        m.triangleList.Add(53);
        m.triangleList.Add(54);
        m.triangleList.Add(17);

        m.triangleList.Add(73);
        m.triangleList.Add(72);
        m.triangleList.Add(35);

        //   print("total number of verticies: " + m.vertexList.Count);


        m.vertexList.Add(new Vector3(-4, 0, 0));//front left 74
        m.vertexList.Add(new Vector3(-4, 4, 0));//front leftup 75
        m.vertexList.Add(new Vector3(4, 4, 0));//front rightup 76
        m.vertexList.Add(new Vector3(4, 0, 0));//front right 77
        m.vertexList.Add(new Vector3(-4, 0, -2));//back left 78
        m.vertexList.Add(new Vector3(-4, 4, -2));//back leftup 79
        m.vertexList.Add(new Vector3(4, 4, -2));//back rightup 80
        m.vertexList.Add(new Vector3(4, 0, -2));//back right 81

        print("total number of verticies: " + m.vertexList.Count);

        m.triangleList.Add(74);
        m.triangleList.Add(75);
        m.triangleList.Add(78);

        m.triangleList.Add(75);
        m.triangleList.Add(79);
        m.triangleList.Add(78);

        m.triangleList.Add(77);
        m.triangleList.Add(81);
        m.triangleList.Add(76);

        m.triangleList.Add(80);
        m.triangleList.Add(76);
        m.triangleList.Add(81);


        m.triangleList.Add(76);
        m.triangleList.Add(80);
        m.triangleList.Add(75);

        m.triangleList.Add(80);
        m.triangleList.Add(79);
        m.triangleList.Add(75);


        for (int i = 0; i < 18; i++)
        {


            m.triangleList.Add(36 + i);
            m.triangleList.Add(55 + i);
            m.triangleList.Add(37 + i);


            m.triangleList.Add(55 + i);
            m.triangleList.Add(56 + i);
            m.triangleList.Add(37 + i);



        }


        #endregion triangleforhorseshoe

        //print("total number of points for horsehoe: " + m.vertexList.Count);

        return m;
    }
    public MeshInfo stairType2(float width, float height, float depth)
    {

        MeshInfo m = new MeshInfo();
        // m.vertexList.Add(new Vector3(-1, -1, 0));//0

        m.vertexList.Add(new Vector3(0, 0, 0));
        m.vertexList.Add(new Vector3(0, height, 0));
        m.vertexList.Add(new Vector3(depth, height, 0));
        m.vertexList.Add(new Vector3(0, 0, width));
        m.vertexList.Add(new Vector3(0, height, width));
        m.vertexList.Add(new Vector3(depth, height, width));

        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);

        m.triangleList.Add(0);
        m.triangleList.Add(3);
        m.triangleList.Add(4);
        m.triangleList.Add(0);
        m.triangleList.Add(4);
        m.triangleList.Add(1);

        m.triangleList.Add(1);
        m.triangleList.Add(4);
        m.triangleList.Add(5);
        m.triangleList.Add(1);
        m.triangleList.Add(5);
        m.triangleList.Add(2);

        m.triangleList.Add(3);
        m.triangleList.Add(5);
        m.triangleList.Add(4);

        m.triangleList.Add(2);
        m.triangleList.Add(3);
        m.triangleList.Add(0);

        m.triangleList.Add(2);
        m.triangleList.Add(5);
        m.triangleList.Add(3);






        return m;


    }
    public MeshInfo stairsRound(float width, float depth, float height, float steps, float degrees)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();
        float h = 0;
        float d = 0;
        float yDegrees = 0;
        for (int i = 0; i < steps; i++, h = height + h, d = depth / 2 + d)
        {

            MeshInfo s1 = stairType2(width, height, depth);
            rotation(s1, 0, yDegrees, 0);
            Translate(s1, new Vector3(0, h, 0));

            meshList.Add(s1);
            yDegrees = yDegrees + degrees;
        }


        MeshInfo combinedMesh = CombineMeshes(meshList);

        float firstX = combinedMesh.vertexList[0].x;
        float firstY = combinedMesh.vertexList[0].y;
        float firstZ = combinedMesh.vertexList[0].z;

        int size_of_vertexList = combinedMesh.vertexList.Count;
        float lastX = combinedMesh.vertexList[size_of_vertexList - 1].x;
        float lastZ = combinedMesh.vertexList[size_of_vertexList - 1].z;
        //print("last x" + lastX);
        // int s = combinedMesh.vertexList.Count; previous
        combinedMesh.vertexList.Add(new Vector3(lastX, firstY, firstZ));
        int s = combinedMesh.vertexList.Count;
        //print("Size of vertexlist");
        //print(s + " S ");
        //print(combinedMesh.vertexList[s]);


        //right Side cover
        //combinedMesh.triangleList.Add(0);
        // combinedMesh.triangleList.Add(s - 5);//previous was s-4
        //combinedMesh.triangleList.Add(s - 1);//previous was s
        combinedMesh.vertexList.Add(new Vector3(lastX, firstY, lastZ));
        s = combinedMesh.vertexList.Count;
        //print(combinedMesh.vertexList[s-1]+"third last");

        //left side cover
        // combinedMesh.triangleList.Add(3);
        // combinedMesh.triangleList.Add(s - 1);
        // combinedMesh.triangleList.Add(s - 3);
        print("last point " + combinedMesh.vertexList[s - 3]);//s-3 is the 5th point of the prism which orderwise is last
        print("second last point " + combinedMesh.vertexList[s - 6]);
        float lastpointX = combinedMesh.vertexList[s - 3].x;
        float lastpointY = combinedMesh.vertexList[s - 3].y;
        float lastpointZ = combinedMesh.vertexList[s - 3].z;

        float secondlastpointX = combinedMesh.vertexList[s - 6].x;
        float secondlastpointY = combinedMesh.vertexList[s - 6].y;
        float secondlastpointZ = combinedMesh.vertexList[s - 6].z;


        combinedMesh.vertexList.Add(new Vector3(depth * 4 + secondlastpointX, secondlastpointY, secondlastpointZ));
        combinedMesh.vertexList.Add(new Vector3(depth * 4 + lastpointX, lastpointY, lastpointZ));
        s = combinedMesh.vertexList.Count;


        /*combinedMesh.triangleList.Add(s - 8);
        combinedMesh.triangleList.Add(s - 5);
        combinedMesh.triangleList.Add(s - 1);
        combinedMesh.triangleList.Add(s - 8);
        combinedMesh.triangleList.Add(s - 1);
        combinedMesh.triangleList.Add(s - 2);
        */

        float fourthPointX = combinedMesh.vertexList[3].x;
        float fourthPointY = combinedMesh.vertexList[3].y;
        float fourthPointZ = combinedMesh.vertexList[3].z;


        combinedMesh.vertexList.Add(new Vector3(firstX - depth * 4, firstY, firstZ));
        combinedMesh.vertexList.Add(new Vector3(fourthPointX - depth * 4, fourthPointY, fourthPointZ));

        s = combinedMesh.vertexList.Count;

        combinedMesh.triangleList.Add(s - 1);
        combinedMesh.triangleList.Add(3);
        combinedMesh.triangleList.Add(0);

        combinedMesh.triangleList.Add(s - 1);
        combinedMesh.triangleList.Add(0);
        combinedMesh.triangleList.Add(s - 2);








        return combinedMesh;

    }
    public MeshInfo window1(int width, int height, int type)
    {


        List<MeshInfo> meshList = new List<MeshInfo>();


        for (int j = 0; j < height; j++)
        {
            float yValue = (float)j;

            for (int i = 0; i < width; i++)
            {

                float xValue = (float)i;

                MeshInfo m = new MeshInfo();
                if (type == 1)
                {
                    m = unitTriangle2();
                }
                else if (type == 2)
                {
                    m = diamond();
                }
                else
                {
                    m = unitPentagon();
                }

                Translate(m, new Vector3(xValue, yValue, 0));

                meshList.Add(m);

            }
        }


        MeshInfo left = unitSquare();
        scale(left, new Vector3(0.25f, height / 2 + 0.5f, 1));
        Translate(left, new Vector3(-0.25f, height / 2, 0));
        meshList.Add(left);

        MeshInfo right = unitSquare();
        scale(right, new Vector3(0.25f, height / 2 + 0.5f, 1));
        Translate(right, new Vector3(width + 0.25f, height / 2, 0));
        meshList.Add(right);

        MeshInfo bottom = unitSquare();
        scale(bottom, new Vector3(width / 2 + 0.5f, 0.25f, 1));
        Translate(bottom, new Vector3(width / 2, -0.25f, 0));
        meshList.Add(bottom);

        MeshInfo top = unitSquare();
        scale(top, new Vector3(width / 2 + 0.5f, 0.25f, 1));
        Translate(top, new Vector3(width / 2, height + 0.25f, 0));
        meshList.Add(top);




        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 0; i < combinedMesh.vertexList.Count; i++)
        {
            combinedMesh.colorList.Add(new Color(0.85f, 0.64f, 0.12f));
        }

        return combinedMesh;
    }
    public MeshInfo unitTriangle2() //making hexagon with two triangles
    {


        MeshInfo m = new MeshInfo();


        m.vertexList.Add(new Vector3(0.5f, 0));
        m.vertexList.Add(new Vector3(0, 0.75f));
        m.vertexList.Add(new Vector3(1, 0.75f));


        m.vertexList.Add(new Vector3(0, 0.25f));
        m.vertexList.Add(new Vector3(0.5f, 1));
        m.vertexList.Add(new Vector3(1, 0.25f));


        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);
        m.triangleList.Add(3);
        m.triangleList.Add(4);
        m.triangleList.Add(5);

        return m;

    }
    public MeshInfo unitTriangle()
    {


        MeshInfo m = new MeshInfo();


        m.vertexList.Add(new Vector3(0, 0));
        m.vertexList.Add(new Vector3(0.5f, 1));
        m.vertexList.Add(new Vector3(1, 0));


        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);

        return m;

    }
    public MeshInfo unitPentagon()
    {

        MeshInfo m = new MeshInfo();


        // rotation(square1, 0, 0, 45);



        m.vertexList.Add(new Vector3(0.5f, 0.5f));//center

        m.vertexList.Add(new Vector3(0.2f, 0));//left bottom

        m.vertexList.Add(new Vector3(0, 0.6f));//left top

        m.vertexList.Add(new Vector3(0.5f, 1));//top

        m.vertexList.Add(new Vector3(1, 0.6f));//right top

        m.vertexList.Add(new Vector3(0.8f, 0));//right bottom



        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);

        m.triangleList.Add(0);
        m.triangleList.Add(2);
        m.triangleList.Add(3);


        m.triangleList.Add(0);
        m.triangleList.Add(3);
        m.triangleList.Add(4);

        m.triangleList.Add(0);
        m.triangleList.Add(4);
        m.triangleList.Add(5);


        m.triangleList.Add(0);
        m.triangleList.Add(5);
        m.triangleList.Add(1);



        return m;

    }
    public MeshInfo unitHexagon()
    {

        MeshInfo m = new MeshInfo();


        // rotation(square1, 0, 0, 45);



        m.vertexList.Add(new Vector3(0.5f, 0.5f));//center

        m.vertexList.Add(new Vector3(0.5f, 0));//bottom

        m.vertexList.Add(new Vector3(0, 0.33f));//left bottom
        m.vertexList.Add(new Vector3(0, 0.66f));//left top

        m.vertexList.Add(new Vector3(0.5f, 1));//top

        m.vertexList.Add(new Vector3(1, 0.66f));//right top

        m.vertexList.Add(new Vector3(1, 0.33f));//right bottom



        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);
        m.triangleList.Add(0);
        m.triangleList.Add(2);
        m.triangleList.Add(3);

        m.triangleList.Add(0);
        m.triangleList.Add(3);
        m.triangleList.Add(4);
        m.triangleList.Add(0);
        m.triangleList.Add(4);
        m.triangleList.Add(5);

        m.triangleList.Add(0);
        m.triangleList.Add(5);
        m.triangleList.Add(6);
        m.triangleList.Add(0);
        m.triangleList.Add(6);
        m.triangleList.Add(1);

        return m;

    }
    public MeshInfo diamond()
    {


        MeshInfo m = new MeshInfo();


        // rotation(square1, 0, 0, 45);



        m.vertexList.Add(new Vector3(0.5f, 0));
        m.vertexList.Add(new Vector3(0, 0.5f));
        m.vertexList.Add(new Vector3(0.5f, 1));
        m.vertexList.Add(new Vector3(1, 0.5f));


        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);
        m.triangleList.Add(0);
        m.triangleList.Add(2);
        m.triangleList.Add(3);



        return m;

    }
    public MeshInfo chair(float width, float height, float length, float LegRadius, float legHeight, int legSectors)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo top = UnitCubeWith8Vertices();

        scale(top, new Vector3(width, height, length));

        meshList.Add(top);
        //reference
        /*
        m.vertexList.Add(new Vector3(-1, -1, 0));//0 needed first leg
        m.vertexList.Add(new Vector3(-1, 1, 0));//1
        m.vertexList.Add(new Vector3(1, 1, 0));//2
        m.vertexList.Add(new Vector3(1, -1, 0));//3 needed second leg

        m.vertexList.Add(new Vector3(-1, -1, 2));//4 neede third leg
        m.vertexList.Add(new Vector3(-1, 1, 2));//5
        m.vertexList.Add(new Vector3(1, 1, 2));//6
        m.vertexList.Add(new Vector3(1, -1, 2));//7*/ //needed fourth leg

        float xStartWidth = top.vertexList[0].x;
        float xEndWidth = top.vertexList[3].x;

        float zfirst = top.vertexList[4].z;
        float zSecond = top.vertexList[7].z;

        MeshInfo leg1 = unitCylinderSects(legSectors, legHeight * 1.5f, LegRadius);
        Translate(leg1, new Vector3(xStartWidth + LegRadius, -legHeight, LegRadius));

        meshList.Add(leg1);

        MeshInfo leg2 = unitCylinderSects(legSectors, legHeight * 1.5f, LegRadius);
        Translate(leg2, new Vector3(xEndWidth - LegRadius, -legHeight, LegRadius));

        meshList.Add(leg2);

        MeshInfo leg3 = unitCylinderSects(legSectors, legHeight * 1.5f, LegRadius);
        Translate(leg3, new Vector3(xStartWidth + LegRadius, -legHeight, zfirst - LegRadius));

        meshList.Add(leg3);

        MeshInfo leg4 = unitCylinderSects(legSectors, legHeight * 1.5f, LegRadius);
        Translate(leg4, new Vector3(xEndWidth - LegRadius, -legHeight, zfirst - LegRadius));

        meshList.Add(leg4);

        MeshInfo back = UnitCubeWith8Vertices();
        scale(back, new Vector3(width, legHeight / 2, LegRadius));

        Translate(back, new Vector3(0, legHeight, zfirst - LegRadius * 2));

        meshList.Add(back);

        MeshInfo rest1 = UnitCubeWith8Vertices();
        scale(rest1, new Vector3(LegRadius, LegRadius, length));
        Translate(rest1, new Vector3(xStartWidth + LegRadius, legHeight * 0.6f, 0));
        meshList.Add(rest1);

        MeshInfo rest2 = UnitCubeWith8Vertices();
        scale(rest2, new Vector3(LegRadius, LegRadius, length));
        Translate(rest2, new Vector3(xEndWidth - LegRadius, legHeight * 0.6f, 0));
        meshList.Add(rest2);

        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 0; i < combinedMesh.vertexList.Count; i++)
        {
            combinedMesh.colorList.Add(new Color(0.54f, 0.27f, 0.07f));

        }

        return combinedMesh;

    }
    public MeshInfo glass(int sectors, float height, float radius)
    {

        //boolean circle 

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo circle1 = unitCircleDownwardsSects(sectors, 360, radius);
        Translate(circle1, new Vector3(0, height, 0));

        MeshInfo circle2 = unitCircleDownwardsSects(sectors, 360, radius);
        meshList.Add(circle1);
        meshList.Add(circle2);

        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 1; i < sectors; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(sectors + 1 + i);
            combinedMesh.triangleList.Add(i + 1);
        }

        combinedMesh.triangleList.Add(sectors);
        combinedMesh.triangleList.Add(sectors * 2 + 1);
        combinedMesh.triangleList.Add(1);

        for (int i = 2; i < sectors; i++)
        {
            combinedMesh.triangleList.Add(sectors + i);
            combinedMesh.triangleList.Add(sectors + i + 1);
            combinedMesh.triangleList.Add(i);
        }
        combinedMesh.triangleList.Add(sectors);
        combinedMesh.triangleList.Add(sectors * 2);
        combinedMesh.triangleList.Add(sectors * 2 + 1);

        combinedMesh.triangleList.Add(1);
        combinedMesh.triangleList.Add(sectors * 2 + 1);
        combinedMesh.triangleList.Add(sectors + 2);


        for (int i = sectors; i > 2; i--)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(sectors - 1 + i);
            combinedMesh.triangleList.Add(i - 1);
        }

        combinedMesh.triangleList.Add(sectors);
        combinedMesh.triangleList.Add(sectors * 2 + 1);
        combinedMesh.triangleList.Add(1);

        print("Count " + combinedMesh.vertexList.Count);

        return combinedMesh;
    }
    public MeshInfo table(float width, float height, float length, float LegRadius, float legHeight, int legSectors)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo top = UnitCubeWith8Vertices();

        scale(top, new Vector3(width, height, length));

        meshList.Add(top);
        //reference
        /*
        m.vertexList.Add(new Vector3(-1, -1, 0));//0 needed first leg
        m.vertexList.Add(new Vector3(-1, 1, 0));//1
        m.vertexList.Add(new Vector3(1, 1, 0));//2
        m.vertexList.Add(new Vector3(1, -1, 0));//3 needed second leg

        m.vertexList.Add(new Vector3(-1, -1, 2));//4 neede third leg
        m.vertexList.Add(new Vector3(-1, 1, 2));//5
        m.vertexList.Add(new Vector3(1, 1, 2));//6
        m.vertexList.Add(new Vector3(1, -1, 2));//7*/ //needed fourth leg


        float xStartWidth = top.vertexList[0].x;
        float xEndWidth = top.vertexList[3].x;

        float zfirst = top.vertexList[4].z;
        float zSecond = top.vertexList[7].z;

        MeshInfo leg1 = unitCylinderSects(legSectors, legHeight, LegRadius);
        Translate(leg1, new Vector3(xStartWidth + LegRadius, -legHeight, LegRadius));

        meshList.Add(leg1);


        MeshInfo leg2 = unitCylinderSects(legSectors, legHeight, LegRadius);
        Translate(leg2, new Vector3(xEndWidth - LegRadius, -legHeight, LegRadius));

        meshList.Add(leg2);

        MeshInfo leg3 = unitCylinderSects(legSectors, legHeight, LegRadius);
        Translate(leg3, new Vector3(xStartWidth + LegRadius, -legHeight, zfirst - LegRadius));

        meshList.Add(leg3);

        MeshInfo leg4 = unitCylinderSects(legSectors, legHeight, LegRadius);
        Translate(leg4, new Vector3(xEndWidth - LegRadius, -legHeight, zfirst - LegRadius));

        meshList.Add(leg4);



        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 0; i < combinedMesh.vertexList.Count; i++)
        {
            combinedMesh.colorList.Add(new Color(1, 0.6f, 0.2f));

        }

        return combinedMesh;

    }
    public MeshInfo unitSquare()
    {

        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(-1, -1));
        m.vertexList.Add(new Vector3(-1, 1));
        m.vertexList.Add(new Vector3(1, 1));
        m.vertexList.Add(new Vector3(1, -1));


        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);
        m.triangleList.Add(0);
        m.triangleList.Add(2);
        m.triangleList.Add(3);


        return m;

    }
    public MeshInfo door()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo d1 = doorParthalf1();
        meshList.Add(d1);


        //second with anti clockwise rotation
        MeshInfo d2 = doorParthalf2();
        rotation(d2, 0, 180, 0);
        meshList.Add(d2);

        //first half second point x because first is middle point

        MeshInfo doorDown = unitSquare();
        Translate(doorDown, new Vector3(0, -1, 0));
        meshList.Add(doorDown);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;
    }
    public MeshInfo doorParthalf2()
    {


        MeshInfo m = new MeshInfo();


        float x = 630;
        float z = 0;
        float y = 0;

        m.vertexList.Add(new Vector3(0, 0, 0));

        for (float i = 1; i < 87; i = i + (360 / 360))
        {

            m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), (Mathf.Sin((z * Mathf.PI) / 180)), 0));
            x = x - (360 / 360);
            z = z + (360 / 360);
            y = y + (360 / 360);

        }

        m.vertexList.Add(new Vector3(0, 1.05f, 0));

        m.vertexList.Add(new Vector3(0, 1.1f, 0));

        m.vertexList.Add(new Vector3(0, 1.5f, 0));
        m.vertexList.Add(new Vector3(0, 1.2f, 0));

        for (int i = 90; i > 0; i--)
        {
            m.triangleList.Add(0);
            m.triangleList.Add(i);
            m.triangleList.Add(i - 1);

        }
        // just swap the end values for the end points
        m.triangleList.Add(0);
        m.triangleList.Add(90);
        m.triangleList.Add(65);



        print(m.vertexList.Count + " points");
        return m;


    }
    public MeshInfo doorParthalf1()
    {


        MeshInfo m = new MeshInfo();


        float x = 630;
        float z = 0;
        float y = 0;

        m.vertexList.Add(new Vector3(0, 0, 0));

        for (float i = 1; i < 87; i = i + (360 / 360))
        {

            m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), (Mathf.Sin((z * Mathf.PI) / 180)), 0));
            x = x - (360 / 360);
            z = z + (360 / 360);
            y = y + (360 / 360);

        }

        m.vertexList.Add(new Vector3(0, 1.05f, 0));

        m.vertexList.Add(new Vector3(0, 1.1f, 0));

        m.vertexList.Add(new Vector3(0, 1.5f, 0));
        m.vertexList.Add(new Vector3(0, 1.2f, 0));

        for (int i = 0; i < 90; i++)
        {
            m.triangleList.Add(0);
            m.triangleList.Add(i);
            m.triangleList.Add(i + 1);

        }

        m.triangleList.Add(0);
        m.triangleList.Add(65);
        m.triangleList.Add(90);



        print(m.vertexList.Count + " points");
        return m;


    }
    public MeshInfo unevenCylinder(float radius1, float radius2, int height)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo circle1 = unitCircleUpwards(20, 360, radius1);
        Translate(circle1, new Vector3(0, height, 0));

        MeshInfo circle2 = unitCircleDownwardsSects(20, 360, radius2);
        meshList.Add(circle1);
        meshList.Add(circle2);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 1; i < 20; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(20 + 1 + i);
            combinedMesh.triangleList.Add(i + 1);
        }

        combinedMesh.triangleList.Add(20);
        combinedMesh.triangleList.Add(20 * 2 + 1);
        combinedMesh.triangleList.Add(1);

        for (int i = 2; i < 20; i++)
        {
            combinedMesh.triangleList.Add(20 + i);
            combinedMesh.triangleList.Add(20 + i + 1);
            combinedMesh.triangleList.Add(i);
        }
        combinedMesh.triangleList.Add(20);
        combinedMesh.triangleList.Add(20 * 2);
        combinedMesh.triangleList.Add(20 * 2 + 1);

        combinedMesh.triangleList.Add(1);
        combinedMesh.triangleList.Add(20 * 2 + 1);
        combinedMesh.triangleList.Add(20 + 2);


        print("Count " + combinedMesh.vertexList.Count);

        return combinedMesh;



    }
    public MeshInfo bezeirCurve()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo cy1 = unevenCylinder(1, 1.77f, 1);
        Translate(cy1, new Vector3(0, 9, 0));
        meshList.Add(cy1);

        MeshInfo cy2 = unevenCylinder(1.77f, 1, 1);
        Translate(cy2, new Vector3(0, 8, 0));
        meshList.Add(cy2);

        MeshInfo cy3 = unevenCylinder(1, 2.3f, 1);
        Translate(cy3, new Vector3(0, 7, 0));
        meshList.Add(cy3);

        MeshInfo cy4 = unevenCylinder(2.3f, 1.5f, 1);
        Translate(cy4, new Vector3(0, 6, 0));
        meshList.Add(cy4);





        MeshInfo combinedMesh = CombineMeshes(meshList);
        return combinedMesh;
    }
    public MeshInfo bezeirCurveShape()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo c1 = circleUpdated(20);
        Translate(c1, new Vector3(0, 10, 0));
        meshList.Add(c1);
        double radiusChange = 0;
        float radiusFloat = 0;


        int height = 4;
        for (int i = 1; i < 5; i++)
        {

            radiusChange = Math.Sqrt(36 - ((i) * (i)));
            radiusFloat = (float)radiusChange;

            MeshInfo c3 = circleOnlyVert(20, radiusFloat);
            Translate(c3, new Vector3(0, height, 0));
            meshList.Add(c3);
            height--;
        }

        for (int i = 9; i > 5; i--)
        {

            radiusChange = Math.Sqrt(36 - ((i - 5) * (i - 5)));
            radiusFloat = (float)radiusChange;

            MeshInfo c2 = circleOnlyVert(20, radiusFloat);
            Translate(c2, new Vector3(0, i, 0));
            meshList.Add(c2);

        }

        MeshInfo center = circleOnlyVert(20, 6);
        Translate(center, new Vector3(0, 5, 0));
        meshList.Add(center);

        MeshInfo down = unitCircleDownwardsSects(20, 360, 1);
        Translate(down, new Vector3(0, 0, 0));
        meshList.Add(down);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 1; i < 20; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
        }

        combinedMesh.triangleList.Add(20);
        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(20);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(1);


        for (int i = 21; i < 40; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(41);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(41);


        for (int i = 41; i < 60; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(61);
        combinedMesh.triangleList.Add(41);
        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(61);


        for (int i = 61; i < 80; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(81);
        combinedMesh.triangleList.Add(61);
        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(81);

        for (int i = 81; i < 100; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(101);
        combinedMesh.triangleList.Add(81);
        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(120);
        combinedMesh.triangleList.Add(101);


        for (int i = 101; i < 120; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(120);
        combinedMesh.triangleList.Add(121);
        combinedMesh.triangleList.Add(101);
        combinedMesh.triangleList.Add(120);
        combinedMesh.triangleList.Add(140);
        combinedMesh.triangleList.Add(121);


        for (int i = 121; i < 140; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(140);
        combinedMesh.triangleList.Add(141);
        combinedMesh.triangleList.Add(121);
        combinedMesh.triangleList.Add(140);
        combinedMesh.triangleList.Add(160);
        combinedMesh.triangleList.Add(141);

        for (int i = 141; i < 160; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(160);
        combinedMesh.triangleList.Add(161);
        combinedMesh.triangleList.Add(141);
        combinedMesh.triangleList.Add(160);
        combinedMesh.triangleList.Add(180);
        combinedMesh.triangleList.Add(161);


        for (int i = 161; i < 180; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(180);
        combinedMesh.triangleList.Add(181);
        combinedMesh.triangleList.Add(161);
        combinedMesh.triangleList.Add(180);
        combinedMesh.triangleList.Add(200);
        combinedMesh.triangleList.Add(181);

        for (int i = 181; i < 200; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(200);
        combinedMesh.triangleList.Add(201);
        combinedMesh.triangleList.Add(181);
        combinedMesh.triangleList.Add(200);
        combinedMesh.triangleList.Add(220);
        combinedMesh.triangleList.Add(201);
        /*
        for (int i = 201; i < 220; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(220);
        combinedMesh.triangleList.Add(221);
        combinedMesh.triangleList.Add(201);
        combinedMesh.triangleList.Add(220);
        combinedMesh.triangleList.Add(240);
        combinedMesh.triangleList.Add(221);
        */
        return combinedMesh;


    }
    public MeshInfo tiltedCylinder(int sectors, int circumference, float radius)
    {


        //boolean circle 

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo circle1 = unitCircleUpwards(sectors, circumference, radius);
        Translate(circle1, new Vector3(0, 10, 0));

        MeshInfo circle2 = unitCircleDownwardsSects(sectors, circumference, radius);
        Translate(circle2, new Vector3(3, 0, 0));
        meshList.Add(circle1);
        meshList.Add(circle2);




        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 1; i < sectors; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(sectors + 1 + i);
            combinedMesh.triangleList.Add(i + 1);
        }

        combinedMesh.triangleList.Add(sectors);
        combinedMesh.triangleList.Add(sectors * 2 + 1);
        combinedMesh.triangleList.Add(1);

        for (int i = 2; i < sectors; i++)
        {
            combinedMesh.triangleList.Add(sectors + i);
            combinedMesh.triangleList.Add(sectors + i + 1);
            combinedMesh.triangleList.Add(i);
        }
        combinedMesh.triangleList.Add(sectors);
        combinedMesh.triangleList.Add(sectors * 2);
        combinedMesh.triangleList.Add(sectors * 2 + 1);

        combinedMesh.triangleList.Add(1);
        combinedMesh.triangleList.Add(sectors * 2 + 1);
        combinedMesh.triangleList.Add(sectors + 2);


        print("Count " + combinedMesh.vertexList.Count);

        return combinedMesh;


    }
    public MeshInfo flower(int levels)
    { //1 or 2

        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo bud = dome(5);
        scale(bud, new Vector3(0.5f, 0.5f, 0.5f));
        meshList.Add(bud);


        MeshInfo petal1 = petal(8, 30);
        Translate(petal1, new Vector3(0, 0, 2.5f));
        meshList.Add(petal1);

        MeshInfo petal2 = petal(8, 30);
        rotation(petal2, 0, 180, 0);
        Translate(petal2, new Vector3(0, 0, -2.5f));
        meshList.Add(petal2);

        MeshInfo petal3 = petal(8, 30);
        rotation(petal3, 0, 90, 0);
        Translate(petal3, new Vector3(2.5f, 0, 0));
        meshList.Add(petal3);

        MeshInfo petal4 = petal(8, 30);
        rotation(petal4, 0, -90, 0);
        Translate(petal4, new Vector3(-2.5f, 0, 0));
        meshList.Add(petal4);

        MeshInfo petal5 = petal(8, 30);
        rotation(petal5, 0, 45, 0);
        Translate(petal5, new Vector3(1.77f, 0, 1.77f));
        meshList.Add(petal5);

        MeshInfo petal6 = petal(8, 30);
        rotation(petal6, 0, 225, 0);
        Translate(petal6, new Vector3(-1.77f, 0, -1.77f));
        meshList.Add(petal6);

        MeshInfo petal7 = petal(8, 30);
        rotation(petal7, 0, -45, 0);
        Translate(petal7, new Vector3(-1.77f, 0, 1.77f));
        meshList.Add(petal7);


        MeshInfo petal8 = petal(8, 30);
        rotation(petal8, 0, 135, 0);
        Translate(petal8, new Vector3(1.77f, 0, -1.77f));
        meshList.Add(petal8);



        if (levels == 2)
        {

            MeshInfo petalb1 = petal(4, 60);
            Translate(petalb1, new Vector3(0, 0, 2.5f));
            meshList.Add(petalb1);

            MeshInfo petalb2 = petal(4, 60);
            rotation(petalb2, 0, 180, 0);
            Translate(petalb2, new Vector3(0, 0, -2.5f));
            meshList.Add(petalb2);

            MeshInfo petalb3 = petal(4, 60);
            rotation(petalb3, 0, 90, 0);
            Translate(petalb3, new Vector3(2.5f, 0, 0));
            meshList.Add(petalb3);

            MeshInfo petalb4 = petal(4, 60);
            rotation(petalb4, 0, -90, 0);
            Translate(petalb4, new Vector3(-2.5f, 0, 0));
            meshList.Add(petalb4);

            MeshInfo petalb5 = petal(4, 60);
            rotation(petalb5, 0, 45, 0);
            Translate(petalb5, new Vector3(1.77f, 0, 1.77f));
            meshList.Add(petalb5);

            MeshInfo petalb6 = petal(4, 60);
            rotation(petalb6, 0, 225, 0);
            Translate(petalb6, new Vector3(-1.77f, 0, -1.77f));
            meshList.Add(petalb6);

            MeshInfo petalb7 = petal(4, 60);
            rotation(petalb7, 0, -45, 0);
            Translate(petalb7, new Vector3(-1.77f, 0, 1.77f));
            meshList.Add(petalb7);


            MeshInfo petalb8 = petal(4, 60);
            rotation(petalb8, 0, 135, 0);
            Translate(petalb8, new Vector3(1.77f, 0, -1.77f));
            meshList.Add(petalb8);


        }

        MeshInfo petal9 = petal(8, 30);
        rotation(petal9, 0, 180, 45);
        scale(petal9, new Vector3(0.5f, 0.5f, 0.5f));
        Translate(petal9, new Vector3(3, -10, 0));
        meshList.Add(petal9);


        MeshInfo stem1 = tiltedCylinder(20, 360, 0.4f);
        Translate(stem1, new Vector3(0, -10, 0));
        //scale(stem, new Vector3(0,20,0));
        meshList.Add(stem1);
        MeshInfo stem2 = unitCylinderSects(20, 10, 0.4f);
        Translate(stem2, new Vector3(3, -20, 0));
        //scale(stem, new Vector3(0,20,0));
        meshList.Add(stem2);


        MeshInfo stem3 = tiltedCylinder(20, 360, 0.4f);
        Translate(stem3, new Vector3(-3, -30, 0));
        rotation(stem3, 0, 180, 0);
        //scale(stem3, new Vector3(0,20,0));
        meshList.Add(stem3);

        MeshInfo combinedMesh = CombineMeshes(meshList);

        return combinedMesh;
    }
    public MeshInfo petal(int length, float degree)
    {


        float x = 630;
        float z = 0;

        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(0, 0, 0));


        for (float i = 1; i < 200; i = i + (360 / 20))
        {

            m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), 0, length * (Mathf.Sin((z * Mathf.PI) / 180))));
            x = x - (360 / 20);
            z = z + (360 / 20);

        }

        float deg = degree * -1;

        rotation(m, deg, 0, 0);

        for (int i = 0; i < 11; i++)
        {
            m.triangleList.Add(0);
            m.triangleList.Add(i);
            m.triangleList.Add(i + 1);

        }


        for (int i = 11; i > 0; i--)
        {
            m.triangleList.Add(0);
            m.triangleList.Add(i);
            m.triangleList.Add(i - 1);

        }




        print(m.vertexList.Count + " Points of petal");


        scale(m, new Vector3(2, 2, 2));
        return m;

    }
    public MeshInfo dome(int radius)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo c1 = circleUpdated(20);
        Translate(c1, new Vector3(0, 6, 0));
        meshList.Add(c1);
        double radiusChange = 0;
        float radiusFloat = 0;

        for (int i = 5; i > 1; i--)
        {

            radiusChange = Math.Sqrt(36 - ((i - 1) * (i - 1)));
            radiusFloat = (float)radiusChange;

            MeshInfo c2 = circleOnlyVert(20, radiusFloat);
            Translate(c2, new Vector3(0, i, 0));
            meshList.Add(c2);

        }

        MeshInfo center = circleOnlyVert(20, 6);
        meshList.Add(center);


        MeshInfo down = unitCircleDownwards(6);
        meshList.Add(down);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 1; i < 20; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
        }

        combinedMesh.triangleList.Add(20);
        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(20);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(1);


        for (int i = 21; i < 40; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(41);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(41);


        for (int i = 41; i < 60; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(61);
        combinedMesh.triangleList.Add(41);
        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(61);


        for (int i = 61; i < 80; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(81);
        combinedMesh.triangleList.Add(61);
        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(81);

        for (int i = 81; i < 100; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(101);
        combinedMesh.triangleList.Add(81);
        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(120);
        combinedMesh.triangleList.Add(101);

        return combinedMesh;

    }
    public MeshInfo sphereUpdated(int radiusSphere)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo c1 = circleUpdated(20);
        Translate(c1, new Vector3(0, 10, 0));
        meshList.Add(c1);
        double radiusChange = 0;
        float radiusFloat = 0;

        for (int i = 9; i > 5; i--)
        {

            radiusChange = Math.Sqrt(36 - ((i - 5) * (i - 5)));
            radiusFloat = (float)radiusChange;

            MeshInfo c2 = circleOnlyVert(20, radiusFloat);
            Translate(c2, new Vector3(0, i, 0));
            meshList.Add(c2);

        }

        MeshInfo center = circleOnlyVert(20, 6);
        Translate(center, new Vector3(0, 5, 0));
        meshList.Add(center);
        int height = 4;
        for (int i = 1; i < 5; i++)
        {

            radiusChange = Math.Sqrt(36 - ((i) * (i)));
            radiusFloat = (float)radiusChange;

            MeshInfo c3 = circleOnlyVert(20, radiusFloat);
            Translate(c3, new Vector3(0, height, 0));
            meshList.Add(c3);
            height--;
        }

        MeshInfo down = unitCircleDownwardsSects(20, 360, 1);
        Translate(down, new Vector3(0, 0, 0));
        meshList.Add(down);


        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 1; i < 20; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
        }

        combinedMesh.triangleList.Add(20);
        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(20);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(1);


        for (int i = 21; i < 40; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(41);
        combinedMesh.triangleList.Add(21);
        combinedMesh.triangleList.Add(40);
        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(41);


        for (int i = 41; i < 60; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(61);
        combinedMesh.triangleList.Add(41);
        combinedMesh.triangleList.Add(60);
        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(61);


        for (int i = 61; i < 80; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(81);
        combinedMesh.triangleList.Add(61);
        combinedMesh.triangleList.Add(80);
        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(81);

        for (int i = 81; i < 100; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(101);
        combinedMesh.triangleList.Add(81);
        combinedMesh.triangleList.Add(100);
        combinedMesh.triangleList.Add(120);
        combinedMesh.triangleList.Add(101);


        for (int i = 101; i < 120; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(120);
        combinedMesh.triangleList.Add(121);
        combinedMesh.triangleList.Add(101);
        combinedMesh.triangleList.Add(120);
        combinedMesh.triangleList.Add(140);
        combinedMesh.triangleList.Add(121);


        for (int i = 121; i < 140; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(140);
        combinedMesh.triangleList.Add(141);
        combinedMesh.triangleList.Add(121);
        combinedMesh.triangleList.Add(140);
        combinedMesh.triangleList.Add(160);
        combinedMesh.triangleList.Add(141);

        for (int i = 141; i < 160; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(160);
        combinedMesh.triangleList.Add(161);
        combinedMesh.triangleList.Add(141);
        combinedMesh.triangleList.Add(160);
        combinedMesh.triangleList.Add(180);
        combinedMesh.triangleList.Add(161);


        for (int i = 161; i < 180; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(180);
        combinedMesh.triangleList.Add(181);
        combinedMesh.triangleList.Add(161);
        combinedMesh.triangleList.Add(180);
        combinedMesh.triangleList.Add(200);
        combinedMesh.triangleList.Add(181);

        for (int i = 181; i < 200; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(200);
        combinedMesh.triangleList.Add(201);
        combinedMesh.triangleList.Add(181);
        combinedMesh.triangleList.Add(200);
        combinedMesh.triangleList.Add(220);
        combinedMesh.triangleList.Add(201);
        /*
        for (int i = 201; i < 220; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 21);
            combinedMesh.triangleList.Add(i + 1);
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(i + 20);
            combinedMesh.triangleList.Add(i + 21);
        }

        combinedMesh.triangleList.Add(220);
        combinedMesh.triangleList.Add(221);
        combinedMesh.triangleList.Add(201);
        combinedMesh.triangleList.Add(220);
        combinedMesh.triangleList.Add(240);
        combinedMesh.triangleList.Add(221);
        */
        return combinedMesh;

    }
    public MeshInfo sphere()
    {

        float height = 9;

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo top = unitCircleUpwards(19, 360, 0);//21 points
        Translate(top, new Vector3(0, 10, 0));

        //scale(top, new Vector3(0.5f, 1, 1)); oval shape
        meshList.Add(top);
        print(meshList[0].vertexList.Count + " points");


        for (int i = 1; i < 6; i++)
        {
            MeshInfo s1 = circleOnlyVert(19, i);
            Translate(s1, new Vector3(0, height, 0));
            meshList.Add(s1);
            height--;
        }

        print(meshList[1].vertexList.Count + " points of second");



        meshList.Add(unitCircleDownwardsSects(19, 360, 0));

        print(meshList[2].vertexList.Count + " points of third");

        MeshInfo combinedMesh = CombineMeshes(meshList);





        return combinedMesh;



    }
    public MeshInfo circleUpdated(int sectors)
    {

        MeshInfo m = new MeshInfo();


        float x = 630;
        float z = 0;

        m.vertexList.Add(new Vector3(0, 0, 0));

        for (float i = 1; i < 360; i = i + (360 / sectors))
        {

            m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), 0, (Mathf.Sin((z * Mathf.PI) / 180))));
            x = x - (360 / sectors);
            z = z + (360 / sectors);

        }

        for (int i = 0; i < (360 / sectors) + 2; i++)
        {
            m.triangleList.Add(0);
            m.triangleList.Add(i);
            m.triangleList.Add(i + 1);

        }
        m.triangleList.Add(0);
        m.triangleList.Add(sectors);
        m.triangleList.Add(1);


        print(m.vertexList.Count + " points");
        return m;

    }
    public MeshInfo circleOnlyVert(int sectors, float radius)
    { //no middle point

        MeshInfo m = new MeshInfo();

        int x = 630;
        int z = 0;

        for (int i = 0; i < 359; i = i + (360 / sectors))
        {
            m.vertexList.Add(new Vector3(radius * (Mathf.Sin((x * Mathf.PI) / 180)), 0, radius * (Mathf.Sin((z * Mathf.PI) / 180))));
            x = x - (360 / sectors);
            z = z + (360 / sectors);

        }

        return m;

    }
    public MeshInfo roman(int width, int length, int no_of_pillars)
    {


        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo baseOfRoman = unitCube2();
        scale(baseOfRoman, new Vector3(width, 1, length));
        meshList.Add(baseOfRoman);

        MeshInfo roofOfRoman = unitCube2();
        scale(roofOfRoman, new Vector3(width, 1, length));
        Translate(roofOfRoman, new Vector3(0, 11, 0));
        meshList.Add(roofOfRoman);


        float valueX = meshList[0].vertexList[1].x;
        float incrementX = 0;
        float incrementZ = 0;
        float constantSpaceWidth = width * 2 / no_of_pillars;
        float constantSpaceLength = length * 2 / no_of_pillars;

        for (int i = 0; i < no_of_pillars; i++)
        {


            MeshInfo p1 = unitCylinderSects(10, 10, 1);
            meshList.Add(p1);
            Translate(p1, new Vector3(valueX + incrementX + 2, 0, 1));

            MeshInfo p2 = unitCylinderSects(10, 10, 1);
            meshList.Add(p2);
            Translate(p2, new Vector3(valueX + incrementX + 2, 0, length * 2 - 1));

            MeshInfo p3 = unitCylinderSects(10, 10, 1);
            meshList.Add(p3);
            Translate(p3, new Vector3(valueX + 1, 0, 4 + incrementZ));

            MeshInfo p4 = unitCylinderSects(10, 10, 1);
            meshList.Add(p4);
            Translate(p4, new Vector3(valueX + width * 2 - 1, 0, 4 + incrementZ));


            incrementX = constantSpaceWidth + incrementX;
            incrementZ = constantSpaceLength + incrementZ;



        }


        MeshInfo top = stair(length * 2, 2 * (Mathf.Sqrt((width * width) / 2)), 2 * (Mathf.Sqrt((width * width) / 2)));
        rotation(top, 0, 0, -45);
        Translate(top, new Vector3(-width, 12, 0));
        //rotation(top, 0, 0, 90);
        meshList.Add(top);

        MeshInfo combinedMesh = CombineMeshes(meshList);


        return combinedMesh;

    }
    public MeshInfo stairsLevels(float levels, float width, float depth, float height, float steps)
    {


        List<MeshInfo> meshList = new List<MeshInfo>();



        for (int i = 0; i < levels; i++)
        {

            if (i % 2 == 0)
            {

                float newHeight = height * steps * i;
                stairs(width, depth, height, steps);

                MeshInfo s1 = stairs(width, depth, height, steps);

                Translate(s1, new Vector3(0, newHeight, 0));
                rotation(s1, 0, 180 * i, 0);
                meshList.Add(s1);
            }
            else
            {

                float newHeight = height * steps * i;
                stairs(width, depth, height, steps);

                MeshInfo s1 = stairs(width, depth, height, steps);

                Translate(s1, new Vector3(depth * steps * -1, newHeight, width * -2));
                rotation(s1, 0, 180 * i, 0);

                meshList.Add(s1);

            }

        }

        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 0; i < combinedMesh.vertexList.Count; i++)
        {
            combinedMesh.colorList.Add(new Color(0.98f, 0.92f, 0.84f));
        }

        return combinedMesh;




    }
    public MeshInfo stairs(float width, float depth, float height, float steps)
    {

        List<MeshInfo> meshList = new List<MeshInfo>();
        float h = 0;
        float d = 0;
        for (int i = 0; i < steps; i++, h = height + h, d = depth + d)
        {

            MeshInfo s1 = stair(width, height, depth);

            Translate(s1, new Vector3(d, h, 0));
            meshList.Add(s1);
        }


        MeshInfo combinedMesh = CombineMeshes(meshList);

        float firstX = combinedMesh.vertexList[0].x;
        float firstY = combinedMesh.vertexList[0].y;
        float firstZ = combinedMesh.vertexList[0].z;

        int size_of_vertexList = combinedMesh.vertexList.Count;
        float lastX = combinedMesh.vertexList[size_of_vertexList - 1].x;
        float lastZ = combinedMesh.vertexList[size_of_vertexList - 1].z;
        //print("last x" + lastX);
        // int s = combinedMesh.vertexList.Count; previous
        combinedMesh.vertexList.Add(new Vector3(lastX, firstY, firstZ));
        int s = combinedMesh.vertexList.Count;
        //print("Size of vertexlist");
        //print(s + " S ");
        //print(combinedMesh.vertexList[s]);
        combinedMesh.triangleList.Add(0);
        combinedMesh.triangleList.Add(s - 5);//previous was s-4
        combinedMesh.triangleList.Add(s - 1);//previous was s
        combinedMesh.vertexList.Add(new Vector3(lastX, firstY, lastZ));
        s = combinedMesh.vertexList.Count;
        //print(combinedMesh.vertexList[s-1]+"third last");
        combinedMesh.triangleList.Add(3);
        combinedMesh.triangleList.Add(s - 1);
        combinedMesh.triangleList.Add(s - 3);
        print("last point " + combinedMesh.vertexList[s - 3]);//s-3 is the 5th point of the prism which orderwise is last
        print("second last point " + combinedMesh.vertexList[s - 6]);
        float lastpointX = combinedMesh.vertexList[s - 3].x;
        float lastpointY = combinedMesh.vertexList[s - 3].y;
        float lastpointZ = combinedMesh.vertexList[s - 3].z;

        float secondlastpointX = combinedMesh.vertexList[s - 6].x;
        float secondlastpointY = combinedMesh.vertexList[s - 6].y;
        float secondlastpointZ = combinedMesh.vertexList[s - 6].z;


        combinedMesh.vertexList.Add(new Vector3(depth * 4 + secondlastpointX, secondlastpointY, secondlastpointZ));
        combinedMesh.vertexList.Add(new Vector3(depth * 4 + lastpointX, lastpointY, lastpointZ));
        s = combinedMesh.vertexList.Count;
        combinedMesh.triangleList.Add(s - 8);
        combinedMesh.triangleList.Add(s - 5);
        combinedMesh.triangleList.Add(s - 1);
        combinedMesh.triangleList.Add(s - 8);
        combinedMesh.triangleList.Add(s - 1);
        combinedMesh.triangleList.Add(s - 2);


        float fourthPointX = combinedMesh.vertexList[3].x;
        float fourthPointY = combinedMesh.vertexList[3].y;
        float fourthPointZ = combinedMesh.vertexList[3].z;


        combinedMesh.vertexList.Add(new Vector3(firstX - depth * 4, firstY, firstZ));
        combinedMesh.vertexList.Add(new Vector3(fourthPointX - depth * 4, fourthPointY, fourthPointZ));

        s = combinedMesh.vertexList.Count;

        combinedMesh.triangleList.Add(s - 1);
        combinedMesh.triangleList.Add(3);
        combinedMesh.triangleList.Add(0);

        combinedMesh.triangleList.Add(s - 1);
        combinedMesh.triangleList.Add(0);
        combinedMesh.triangleList.Add(s - 2);




        return combinedMesh;

    }
    public MeshInfo point(float x, float y, float z)
    {

        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(x, y, z));


        return m;

    }
    public MeshInfo stair(float width, float height, float depth)
    {

        MeshInfo m = new MeshInfo();
        // m.vertexList.Add(new Vector3(-1, -1, 0));//0

        m.vertexList.Add(new Vector3(0, 0, 0));
        m.vertexList.Add(new Vector3(0, height, 0));
        m.vertexList.Add(new Vector3(depth, height, 0));
        m.vertexList.Add(new Vector3(0, 0, width));
        m.vertexList.Add(new Vector3(0, height, width));
        m.vertexList.Add(new Vector3(depth, height, width));

        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);

        m.triangleList.Add(0);
        m.triangleList.Add(3);
        m.triangleList.Add(4);
        m.triangleList.Add(0);
        m.triangleList.Add(4);
        m.triangleList.Add(1);

        m.triangleList.Add(1);
        m.triangleList.Add(4);
        m.triangleList.Add(5);
        m.triangleList.Add(1);
        m.triangleList.Add(5);
        m.triangleList.Add(2);

        m.triangleList.Add(3);
        m.triangleList.Add(5);
        m.triangleList.Add(4);







        return m;


    }
    public MeshInfo square()
    {
        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(-1, -1, 0));//0
        m.vertexList.Add(new Vector3(-1, 1, 0));//1
        m.vertexList.Add(new Vector3(1, 1, 0));//2
        m.vertexList.Add(new Vector3(1, -1, 0));//3


        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);
        m.triangleList.Add(0);
        m.triangleList.Add(2);
        m.triangleList.Add(3);

        return m;

    }
    public MeshInfo tri()
    {

        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(-1, -1, 0));//0
        m.vertexList.Add(new Vector3(-1, 1, 0));//1
        m.vertexList.Add(new Vector3(1, 1, 0));//2

        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);

        return m;
    }
    public MeshInfo unitCircleDownwardsSects(int sectors, int circumference, float radius)
    {

        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(0, 0, 0));

        int x = 630;
        int z = 0;

        for (int i = 1; i < 359; i = i + (360 / sectors))
        {
            m.vertexList.Add(new Vector3((radius * Mathf.Sin((x * Mathf.PI) / 180)), 0, (radius * Mathf.Sin((z * Mathf.PI) / 180))));
            x = x - (360 / sectors);
            z = z + (360 / sectors);

        }

        for (int i = sectors + 1; i > 2; i--)
        {
            m.triangleList.Add(0);
            m.triangleList.Add(i - 1);
            m.triangleList.Add(i - 2);
        }

        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(sectors);



        return m;

    }
    public MeshInfo unitCircleDownwards(float radius)
    {



        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(0, 0, 0));


        int x = 630;
        int z = 0;

        for (int i = 0; i < 359; i++)
        {
            m.vertexList.Add(new Vector3(radius * (Mathf.Sin((x * Mathf.PI) / 180)), 0, radius * (Mathf.Sin((z * Mathf.PI) / 180))));
            x--;
            z++;

        }

        for (int i = 360; i > 2; i--)
        {
            m.triangleList.Add(0);
            m.triangleList.Add(i - 1);
            m.triangleList.Add(i - 2);
        }

        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(359);

        return m;


    }
    public MeshInfo unitCircleUpwards(int sectors, int circumference, float radius)
    {


        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(0, 0, 0));

        int x = 630;
        int z = 0;

        for (int i = 1; i < 359; i = i + (360 / sectors))
        {
            m.vertexList.Add(new Vector3((radius * Mathf.Sin((x * Mathf.PI) / 180)), 0, (radius * Mathf.Sin((z * Mathf.PI) / 180))));
            x = x - (360 / sectors);
            z = z + (360 / sectors);

        }

        for (int i = 1; i < (circumference / (360 / sectors)); i++)
        {
            m.triangleList.Add(0);
            m.triangleList.Add(i);
            m.triangleList.Add(i + 1);

        }
        m.triangleList.Add(0);
        m.triangleList.Add(sectors);
        m.triangleList.Add(1);

        return m;

    }
    public MeshInfo unitCylinderSects(int sectors, float height, float radius)
    {


        //boolean circle 

        List<MeshInfo> meshList = new List<MeshInfo>();

        MeshInfo circle1 = unitCircleUpwards(sectors, 360, radius);
        Translate(circle1, new Vector3(0, height, 0));

        MeshInfo circle2 = unitCircleDownwardsSects(sectors, 360, radius);
        meshList.Add(circle1);
        meshList.Add(circle2);




        MeshInfo combinedMesh = CombineMeshes(meshList);

        for (int i = 1; i < sectors; i++)
        {
            combinedMesh.triangleList.Add(i);
            combinedMesh.triangleList.Add(sectors + 1 + i);
            combinedMesh.triangleList.Add(i + 1);
        }

        combinedMesh.triangleList.Add(sectors);
        combinedMesh.triangleList.Add(sectors * 2 + 1);
        combinedMesh.triangleList.Add(1);

        for (int i = 2; i < sectors; i++)
        {
            combinedMesh.triangleList.Add(sectors + i);
            combinedMesh.triangleList.Add(sectors + i + 1);
            combinedMesh.triangleList.Add(i);
        }
        combinedMesh.triangleList.Add(sectors);
        combinedMesh.triangleList.Add(sectors * 2);
        combinedMesh.triangleList.Add(sectors * 2 + 1);

        combinedMesh.triangleList.Add(1);
        combinedMesh.triangleList.Add(sectors * 2 + 1);
        combinedMesh.triangleList.Add(sectors + 2);


        print("Count " + combinedMesh.vertexList.Count);

        return combinedMesh;

    }
    public MeshInfo UnitCube()
    {

        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(-1, 1, 2));
        m.vertexList.Add(new Vector3(1, 1, 2));
        m.vertexList.Add(new Vector3(-1, -1, 2));
        m.vertexList.Add(new Vector3(1, -1, 2));

        //BACK FACE


        m.vertexList.Add(new Vector3(1, 1, 0));
        m.vertexList.Add(new Vector3(-1, 1, 0));
        m.vertexList.Add(new Vector3(1, -1, 0));
        m.vertexList.Add(new Vector3(-1, -1, 0));

        //LEFT FACE



        m.vertexList.Add(new Vector3(-1, 1, 0));
        m.vertexList.Add(new Vector3(-1, 1, 2));
        m.vertexList.Add(new Vector3(-1, -1, 0));
        m.vertexList.Add(new Vector3(-1, -1, 2));

        //RIGHT FACE




        m.vertexList.Add(new Vector3(1, 1, 2));
        m.vertexList.Add(new Vector3(1, 1, 0));
        m.vertexList.Add(new Vector3(1, -1, 2));
        m.vertexList.Add(new Vector3(1, -1, 0));

        //TOP FACE


        m.vertexList.Add(new Vector3(-1, 1, 0));
        m.vertexList.Add(new Vector3(1, 1, 0));
        m.vertexList.Add(new Vector3(-1, 1, 2));
        m.vertexList.Add(new Vector3(1, 1, 2));

        //BOTTOM FACE



        m.vertexList.Add(new Vector3(-1, -1, 2));
        m.vertexList.Add(new Vector3(1, -1, 2));
        m.vertexList.Add(new Vector3(-1, -1, 0));
        m.vertexList.Add(new Vector3(1, -1, 0));




        //triangles




        //front face

        m.triangleList.Add(0);
        m.triangleList.Add(2);
        m.triangleList.Add(3);

        m.triangleList.Add(3);
        m.triangleList.Add(1);
        m.triangleList.Add(0);




        //back face

        m.triangleList.Add(4);
        m.triangleList.Add(6);
        m.triangleList.Add(7);

        m.triangleList.Add(7);
        m.triangleList.Add(5);
        m.triangleList.Add(4);




        //left  face

        m.triangleList.Add(8);
        m.triangleList.Add(10);
        m.triangleList.Add(11);

        m.triangleList.Add(11);
        m.triangleList.Add(9);
        m.triangleList.Add(8);



        //right face

        m.triangleList.Add(12);
        m.triangleList.Add(14);
        m.triangleList.Add(15);

        m.triangleList.Add(15);
        m.triangleList.Add(13);
        m.triangleList.Add(12);


        //top face

        m.triangleList.Add(16);
        m.triangleList.Add(18);
        m.triangleList.Add(19);


        m.triangleList.Add(19);
        m.triangleList.Add(17);
        m.triangleList.Add(16);

        //bottom face

        m.triangleList.Add(20);
        m.triangleList.Add(22);
        m.triangleList.Add(23);

        m.triangleList.Add(23);
        m.triangleList.Add(21);
        m.triangleList.Add(20);

        return m;
    }
    public MeshInfo UnitCylinder()
    {
        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(0, 10, 0));


        int x = 630;
        int z = 0;

        for (int i = 0; i < 359; i++)
        {
            m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), 10, (Mathf.Sin((z * Mathf.PI) / 180))));
            x--;
            z++;

        }
        x = 630;
        z = 0;

        m.vertexList.Add(new Vector3(0, 0, 0));

        for (int i = 360; i < 719; i++)
        {
            m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), 0, (Mathf.Sin((z * Mathf.PI) / 180))));
            x--;
            z++;

        }

        for (int i = 0; i < 358; i++)
        {
            m.triangleList.Add(0);
            m.triangleList.Add(i + 1);
            m.triangleList.Add(i + 2);

        }


        m.triangleList.Add(0);
        m.triangleList.Add(359);
        m.triangleList.Add(1);



        for (int i = 720; i > 362; i--)
        {
            m.triangleList.Add(360);
            m.triangleList.Add(i - 1);
            m.triangleList.Add(i - 2);
        }

        m.triangleList.Add(360);
        m.triangleList.Add(361);
        m.triangleList.Add(719);


        for (int i = 1; i < 359; i++)
        {
            m.triangleList.Add(i);
            m.triangleList.Add(i + 1);
            m.triangleList.Add(i + 360);

        }
        for (int i = 361; i < 719; i++)
        {
            m.triangleList.Add(i);
            m.triangleList.Add(i + 1);
            m.triangleList.Add(i - 361);

        }

        return m;
    }
    public MeshInfo UnitCone()
    {
        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(0, 1, 0));


        int x = 630;
        int z = 0;

        for (int i = 0; i < 359; i++)
        {
            m.vertexList.Add(new Vector3((Mathf.Sin((x * Mathf.PI) / 180)), 0, (Mathf.Sin((z * Mathf.PI) / 180))));
            x--;
            z++;

        }


        for (int i = 0; i < 358; i++)
        {
            m.triangleList.Add(0);
            m.triangleList.Add(i + 1);
            m.triangleList.Add(i + 2);

        }


        m.triangleList.Add(0);
        m.triangleList.Add(359);
        m.triangleList.Add(1);


        m.vertexList.Add(new Vector3(0, 0, 0));//360

        for (int i = 360; i > 2; i--)
        {
            m.triangleList.Add(360);
            m.triangleList.Add(i - 1);
            m.triangleList.Add(i - 2);
        }

        m.triangleList.Add(360);
        m.triangleList.Add(1);
        m.triangleList.Add(359);

        return m;

    }
    void Translate(MeshInfo m, Vector3 offset)
    {
        for (int i = 0; i < m.vertexList.Count; i++)
        {
            m.vertexList[i] += offset;
        }
    }
    /*
    void Translate(MeshInfo m, Vector3 offset)
    {
        Matrix4x4 translate = ..... fill this up ....... with offset

        for (int i = 0; i < m.vertexList.Count; i++)
        {
            Vector4 v = new Vector4(m.vertexList[i].x, m.vertexList[i].y, m.vertexList[i].z, 1);
            Vector4 newV = translate * v;

            m.vertexList[i].x = newV.x;
            m.vertexList[i].y = newV.y;
            m.vertexList[i].z = newV.z;
        }
    }
    */
    /*
        void Rotate(MeshInfo m, float angle, Vec3 axis)
        {
            Matrix4x4 rotate = ..... fill this up ....... with angle and axis

            for (int i = 0; i < m.vertexList.Count; i++)
            {
                Vector4 v = new Vector4(m.vertexList[i].x, m.vertexList[i].y, m.vertexList[i].z, 1);
                Vector4 newV = rotate * v;

                m.vertexList[i].x = newV.x;
                m.vertexList[i].y = newV.y;
                m.vertexList[i].z = newV.z;
            }
        }
        */
    /*
void Scale(MeshInfo m, float scaleFactor)
{
    Matrix4x4 scale = ..... fill this up ....... with Scale factor

    for (int i = 0; i < m.vertexList.Count; i++)
    {
        Vector4 v = new Vector4(m.vertexList[i].x, m.vertexList[i].y, m.vertexList[i].z, 1);
        Vector4 newV = scale * v;

        m.vertexList[i].x = newV.x;
        m.vertexList[i].y = newV.y;
        m.vertexList[i].z = newV.z;
    }
}
*/
    //sir did this
    //void Translate(Mesh m, Vector3 offset)
    //{

    //    List<Vector3> vList = new List<Vector3>();
    //    m.GetVertices(vList);

    //    Matrix4x4 mat = new Matrix4x4();
    //    Matrix4x4 mat2 = new Matrix4x4();

    //    mat* mat2

    //    Vector4 v4;
    //    mat* v4

    //    for (int i = 0; i < vList.Count; i++)
    //    {
    //        vList[i]

    //    }

    //    offset.x;

    //}

    public MeshInfo UnitCubeWith8Vertices()
    {

        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(-1, -1, 0));//0
        m.vertexList.Add(new Vector3(-1, 1, 0));//1
        m.vertexList.Add(new Vector3(1, 1, 0));//2
        m.vertexList.Add(new Vector3(1, -1, 0));//3

        m.vertexList.Add(new Vector3(-1, -1, 2));//4
        m.vertexList.Add(new Vector3(-1, 1, 2));//5
        m.vertexList.Add(new Vector3(1, 1, 2));//6
        m.vertexList.Add(new Vector3(1, -1, 2));//7

        //front
        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);

        m.triangleList.Add(0);
        m.triangleList.Add(2);
        m.triangleList.Add(3);

        //back
        m.triangleList.Add(4);
        m.triangleList.Add(6);
        m.triangleList.Add(5);

        m.triangleList.Add(4);
        m.triangleList.Add(7);
        m.triangleList.Add(6);

        //right

        m.triangleList.Add(2);
        m.triangleList.Add(6);
        m.triangleList.Add(7);

        m.triangleList.Add(2);
        m.triangleList.Add(7);
        m.triangleList.Add(3);

        //left
        m.triangleList.Add(4);
        m.triangleList.Add(5);
        m.triangleList.Add(1);

        m.triangleList.Add(4);
        m.triangleList.Add(1);
        m.triangleList.Add(0);

        //top

        m.triangleList.Add(1);
        m.triangleList.Add(5);
        m.triangleList.Add(6);

        m.triangleList.Add(1);
        m.triangleList.Add(6);
        m.triangleList.Add(2);

        //bottom

        m.triangleList.Add(0);
        m.triangleList.Add(3);
        m.triangleList.Add(7);

        m.triangleList.Add(0);
        m.triangleList.Add(7);
        m.triangleList.Add(4);

        // MeshFilter mf = GetComponent<MeshFilter>();
        // Mesh m = mf.mesh;
        //Mesh m = new Mesh(); 
        //m.vertices = pList.ToArray();
        //m.triangles = triangleList.ToArray();


        return m;
    }



    public MeshInfo cube()
    {
        MeshInfo m = new MeshInfo();



        //front face


        m.vertexList.Add(new Vector3(-1, 1, 1));
        m.vertexList.Add(new Vector3(1, 1, 1));
        m.vertexList.Add(new Vector3(-1, -1, 1));
        m.vertexList.Add(new Vector3(1, -1, 1));

        //BACK FACE


        m.vertexList.Add(new Vector3(1, 1, -1));
        m.vertexList.Add(new Vector3(-1, 1, -1));
        m.vertexList.Add(new Vector3(1, -1, -1));
        m.vertexList.Add(new Vector3(-1, -1, -1));

        //LEFT FACE



        m.vertexList.Add(new Vector3(-1, 1, -1));
        m.vertexList.Add(new Vector3(-1, 1, 1));
        m.vertexList.Add(new Vector3(-1, -1, -1));
        m.vertexList.Add(new Vector3(-1, -1, 1));

        //RIGHT FACE




        m.vertexList.Add(new Vector3(1, 1, 1));
        m.vertexList.Add(new Vector3(1, 1, -1));
        m.vertexList.Add(new Vector3(1, -1, 1));
        m.vertexList.Add(new Vector3(1, -1, -1));

        //TOP FACE


        m.vertexList.Add(new Vector3(-1, 1, -1));
        m.vertexList.Add(new Vector3(1, 1, -1));
        m.vertexList.Add(new Vector3(-1, 1, 1));
        m.vertexList.Add(new Vector3(1, 1, 1));

        //BOTTOM FACE



        m.vertexList.Add(new Vector3(-1, -1, 1));
        m.vertexList.Add(new Vector3(1, -1, 1));
        m.vertexList.Add(new Vector3(-1, -1, -1));
        m.vertexList.Add(new Vector3(1, -1, -1));




        //triangles




        //front face

        m.triangleList.Add(0);
        m.triangleList.Add(2);
        m.triangleList.Add(3);

        m.triangleList.Add(3);
        m.triangleList.Add(1);
        m.triangleList.Add(0);




        //back face

        m.triangleList.Add(4);
        m.triangleList.Add(6);
        m.triangleList.Add(7);

        m.triangleList.Add(7);
        m.triangleList.Add(5);
        m.triangleList.Add(4);




        //left  face

        m.triangleList.Add(8);
        m.triangleList.Add(10);
        m.triangleList.Add(11);

        m.triangleList.Add(11);
        m.triangleList.Add(9);
        m.triangleList.Add(8);



        //right face

        m.triangleList.Add(12);
        m.triangleList.Add(14);
        m.triangleList.Add(15);

        m.triangleList.Add(15);
        m.triangleList.Add(13);
        m.triangleList.Add(12);


        //top face

        m.triangleList.Add(16);
        m.triangleList.Add(18);
        m.triangleList.Add(19);


        m.triangleList.Add(19);
        m.triangleList.Add(17);
        m.triangleList.Add(16);

        //bottom face

        m.triangleList.Add(20);
        m.triangleList.Add(22);
        m.triangleList.Add(23);

        m.triangleList.Add(23);
        m.triangleList.Add(21);
        m.triangleList.Add(20);


        return m;
    }
    public MeshInfo carpet()
    {
        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo design = window1(4, 4, 2);


        rotation(design, 90, -90, 0);



        //for (int i = 0; i < design.vertexList.Count; i++)
        //{
        //    design.colorList.Add(Color.red);
        //}

        meshList.Add(design);


        MeshInfo combinedMesh = CombineMeshes(meshList);



        return combinedMesh;
    }
    public MeshInfo dewan()
    {
        //List<MeshInfo> meshlist = new List<MeshInfo>();
        List<MeshInfo> meshList = new List<MeshInfo>();

        #region methods


        MeshInfo dewan = horseShoe();
        MeshInfo dewanback = horseShoe();
        MeshInfo dewanback_amend_Left = square();
        MeshInfo dewanback_amend_Right = square();


        #endregion methods

        #region transform
        //plane


        //dewan
        scale(dewan, new Vector3(1, 1, 3));
        Translate(dewan, new Vector3(0, 0, 6));

        //dewanback
        rotation(dewanback, -90, 0, 0);
        scale(dewanback, new Vector3(1, 1, 1.5f));
        Translate(dewanback, new Vector3(0, 6, 6));
        //dewan left

        rotation(dewanback_amend_Left, 0, 180, 0);
        Translate(dewanback_amend_Left, new Vector3(-7, 5, 6));
        scale(dewanback_amend_Left, new Vector3(0.5f, 1, 1));

        //dewan right

        rotation(dewanback_amend_Right, 0, 180, 0);

        Translate(dewanback_amend_Right, new Vector3(7, 5, 6));
        scale(dewanback_amend_Right, new Vector3(0.5f, 1, 1));
        #endregion transform


        #region adding    


        #endregion adding


        meshList.Add(dewan);
        meshList.Add(dewanback);
        meshList.Add(dewanback_amend_Left);
        meshList.Add(dewanback_amend_Right);

        MeshInfo combinedMesh = CombineMeshes(meshList);
        return combinedMesh;


    }
    public MeshInfo plane()
    {
        List<MeshInfo> meshList = new List<MeshInfo>();



        MeshInfo plane1 = UnitCube();

        //for (int i = 0; i < plane1.vertexList.Count; i++)
        //{
        //    plane1.colorList.Add(Color.gray);
        //}

        scale(plane1, new Vector3(40, 1, 40));
        meshList.Add(plane1);

        MeshInfo combinedMesh = CombineMeshes(meshList);
        return combinedMesh;
    }
    public MeshInfo sector1()
    {

        List<MeshInfo> meshList = new List<MeshInfo>();

        #region methods

        MeshInfo plane1 = cube();

        for (int i = 0; i < plane1.vertexList.Count; i++)
        {
            plane1.colorList.Add(Color.gray);
        }


        MeshInfo dewaan = dewan();
        for (int i = 0; i < dewaan.vertexList.Count; i++)
        {
            dewaan.colorList.Add(new Color(1.0f, 0.5f, 0.0f));
        }

        MeshInfo kaleen = carpet();

        for (int i = 0; i < kaleen.vertexList.Count; i++)
        {
            kaleen.colorList[i] = (new Color(0.6f, 0.6f, 0.6f));
        }

        MeshInfo tabl = table(7, 0.2f, 4, 0.5f, 7, 20);
        /*
        for (int i = 0; i < tabl.vertexList.Count; i++)
        {
            tabl.colorList.Add(new Color(1, 0.6f, 0.2f));
        }
        */
        MeshInfo tabl2 = table(7, 0.2f, 4, 0.5f, 7, 20);

        /*for (int i = 0; i < tabl2.vertexList.Count; i++)
        {
            tabl2.colorList.Add(new Color(1, 0.6f, 0.2f));
        }
        */
        MeshInfo gul = guldan();
        MeshInfo gul2 = guldan();

        //MeshInfo kaleen2 = window1(4,4,2);
        //for (int i = 0; i < kaleen2.vertexList.Count; i++)
        //{
        //    kaleen2.colorList.Add(Color.yellow);
        //}

        #endregion methods

        #region transform
        //plane
        scale(plane1, new Vector3(40, 1, 40));



        //dewan
        Translate(dewaan, new Vector3(0, 1, -38));

        //carpet

        scale(kaleen, new Vector3(15, 1, 10));
        Translate(kaleen, new Vector3(30, 1.5f, -10));

        //table
        Translate(tabl, new Vector3(-28, 6, -36));
        Translate(tabl2, new Vector3(28, 6, -36));

        //guldan

        Translate(gul, new Vector3(-28, 6, -31));
        Translate(gul2, new Vector3(28, 6, -31));

        //scale(kaleen2, new Vector3(10, 1, 15));
        //Translate(kaleen2, new Vector3(10, 1.1f, -30));
        //  rotation(kaleen2, 0, 90, 0);
        // Translate(carpet, new Vector3(0, 1, 45));



        #endregion transform


        #region adding    

        meshList.Add(plane1);
        meshList.Add(dewaan);
        meshList.Add(kaleen);
        meshList.Add(tabl);
        meshList.Add(tabl2);
        meshList.Add(gul);
        meshList.Add(gul2);


        #endregion adding






        MeshInfo combinedMesh = CombineMeshes(meshList);
        return combinedMesh;
    }
    public MeshInfo guldan()
    {
        List<MeshInfo> meshList = new List<MeshInfo>();


        MeshInfo f = flower(2);

        for (int i = 0; i < f.vertexList.Count; i++)
        {
            f.colorList.Add(Color.red);
        }


        MeshInfo c = unitCylinderSects(15, 2, 2);
        for (int i = 0; i < c.vertexList.Count; i++)
        {
            c.colorList.Add(new Color(1.0f, 0.5f, 0.0f));
        }


        MeshInfo d = dount(1, 1f, 16, 20);
        for (int i = 0; i < d.vertexList.Count; i++)
        {
            d.colorList.Add(Color.cyan);
        }


        Translate(d, new Vector3(0, 2, 0));

        scale(f, new Vector3(0.1f, 0.1f, 0.1f));
        Translate(f, new Vector3(-0.3f, 4, 0));

        meshList.Add(c);
        meshList.Add(d);

        meshList.Add(f);

        MeshInfo combinedMesh = CombineMeshes(meshList);
        return combinedMesh;
    }
    void RST(MeshInfo m, float angleX, float angleY, float angleZ, Vector3 Nscale, Vector3 Ntranslation)
    {
        Vector3 translation = Ntranslation;
        //  Vector3 eulerAngles;
        Vector3 scale = Nscale;

        Quaternion rotation = Quaternion.Euler(angleX, angleY, angleZ);
        Matrix4x4 mm = Matrix4x4.TRS(translation, rotation, scale);
        print(mm);
        int i = 0;
        while (i < m.vertexList.Count)
        {
            m.vertexList[i] = mm.MultiplyPoint3x4(m.vertexList[i]);
            i++;
        }

    }
    void rotation(MeshInfo m, float angleX, float angleY, float angleZ)
    {
        Vector3 translation = new Vector3(0, 0, 0);
        Vector3 scale = new Vector3(1, 1, 1);

        Quaternion rotation = Quaternion.Euler(angleX, angleY, angleZ);
        Matrix4x4 mm = Matrix4x4.TRS(translation, rotation, scale);
        int i = 0;
        while (i < m.vertexList.Count)
        {
            m.vertexList[i] = mm.MultiplyPoint3x4(m.vertexList[i]);
            i++;
        }
    }


    public MeshInfo unitCube2()
    {
        MeshInfo m = new MeshInfo();

        m.vertexList.Add(new Vector3(-1, -1, 0));//0
        m.vertexList.Add(new Vector3(-1, 1, 0));//1
        m.vertexList.Add(new Vector3(1, 1, 0));//2
        m.vertexList.Add(new Vector3(1, -1, 0));//3

        m.vertexList.Add(new Vector3(-1, -1, 2));//4
        m.vertexList.Add(new Vector3(-1, 1, 2));//5
        m.vertexList.Add(new Vector3(1, 1, 2));//6
        m.vertexList.Add(new Vector3(1, -1, 2));//7

        //front
        m.triangleList.Add(0);
        m.triangleList.Add(1);
        m.triangleList.Add(2);

        m.triangleList.Add(0);
        m.triangleList.Add(2);
        m.triangleList.Add(3);

        //back
        m.triangleList.Add(4);
        m.triangleList.Add(6);
        m.triangleList.Add(5);

        m.triangleList.Add(4);
        m.triangleList.Add(7);
        m.triangleList.Add(6);

        //right

        m.triangleList.Add(2);
        m.triangleList.Add(6);
        m.triangleList.Add(7);

        m.triangleList.Add(2);
        m.triangleList.Add(7);
        m.triangleList.Add(3);

        //left
        m.triangleList.Add(4);
        m.triangleList.Add(5);
        m.triangleList.Add(1);

        m.triangleList.Add(4);
        m.triangleList.Add(1);
        m.triangleList.Add(0);

        //top

        m.triangleList.Add(1);
        m.triangleList.Add(5);
        m.triangleList.Add(6);

        m.triangleList.Add(1);
        m.triangleList.Add(6);
        m.triangleList.Add(2);

        //bottom

        m.triangleList.Add(0);
        m.triangleList.Add(3);
        m.triangleList.Add(7);

        m.triangleList.Add(0);
        m.triangleList.Add(7);
        m.triangleList.Add(4);

        // MeshFilter mf = GetComponent<MeshFilter>();
        // Mesh m = mf.mesh;
        //Mesh m = new Mesh(); 
        //m.vertices = pList.ToArray();
        //m.triangles = triangleList.ToArray();

        for (int i = 0; i < m.vertexList.Count; i++)
        {
            m.colorList.Add(new Color(1, 1, 1, 0.5f));
        }

        return m;
    }
    void scale(MeshInfo m, Vector3 Nscale)
    {
        Vector3 translation = new Vector3(0, 0, 0);
        Vector3 scale = Nscale;

        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        Matrix4x4 mm = Matrix4x4.TRS(translation, rotation, scale);
        int i = 0;
        while (i < m.vertexList.Count)
        {
            m.vertexList[i] = mm.MultiplyPoint3x4(m.vertexList[i]);
            i++;
        }
    }
    void update()
    {

    }


}