using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stapler : MonoBehaviour
{
    [Header("Stapler")]
    [SerializeField] int maxStaples = 5;
    int staples;
    List<Paper> selectedPapers = new List<Paper>();
    [SerializeField] int maxPaperSelect = 2; // max number of papers that can be stapled at once
    [SerializeField] GameObject stapledPapersPrefab;

    Camera cam;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI staplesText;

    // Start is called before the first frame update
    void Start()
    {
        staples = maxStaples;
        UpdateStaplesText();

        // max paper select should be no lower than 2
        if (maxPaperSelect < 2)
        {
            Debug.LogWarning("maxPaperSelect property for stapler is less than 2. Updating it to 2.");
            maxPaperSelect = 2;
        }

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.CompareTag("Paper"))
                {
                    Paper paper = hitInfo.collider.gameObject.GetComponent<Paper>();
                    ToggleSelectPaper(paper);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Staple();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public bool ToggleSelectPaper(Paper paper)
    {
        if (paper.stapled || (!paper.IsSelected() && selectedPapers.Count >= maxPaperSelect)) return false;

        if (paper.IsSelected())
        {
            selectedPapers.Remove(paper);
            paper.ToggleSelect(false);
        }
        else
        {
            selectedPapers.Add(paper);
            paper.ToggleSelect(true);
        }

        return true;
    }

    public void Staple()
    {
        if (staples <= 0 || selectedPapers.Count < 2) return;

        // stack papers with most recently selected paper on bottom
        Paper bottomPaper = selectedPapers[selectedPapers.Count - 1];
        GameObject obj = Instantiate(stapledPapersPrefab, bottomPaper.transform.position, bottomPaper.transform.rotation);
        StapledPaperStack stapledPaperStack = obj.GetComponent<StapledPaperStack>();
        for (int i = selectedPapers.Count - 1; i >= 0; i--)
        {
            stapledPaperStack.AddPaper(selectedPapers[i]);
            selectedPapers[i].stapled = true;
            selectedPapers[i].ToggleSelect(false);
            selectedPapers.Remove(selectedPapers[i]);
        }

        staples--;
        UpdateStaplesText();
    }

    void Reload()
    {
        staples = maxStaples;
        UpdateStaplesText();
    }

    void UpdateStaplesText()
    {
        staplesText.SetText($"Staples: {staples}/{maxStaples}");
    }

}
