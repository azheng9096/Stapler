using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StapledPaperStack : MonoBehaviour
{
    List<Paper> papers = new List<Paper>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPaper(Paper paper)
    {
        papers.Add(paper);
        paper.gameObject.transform.SetParent(transform);
        paper.gameObject.transform.localPosition = new Vector3(0, 0.2f * (papers.Count - 1), 0);
        paper.gameObject.transform.localRotation = Quaternion.identity;

        // add joint to connect the two most recent papers
        if (papers.Count >= 2)
        {
            GameObject prevPaper = papers[papers.Count - 2].gameObject;
            prevPaper.AddComponent<FixedJoint>();
            prevPaper.GetComponent<FixedJoint>().connectedBody = paper.gameObject.GetComponent<Rigidbody>();
        }
    }
}
