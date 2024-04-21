using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class BookDisplayer : MonoBehaviour
{
    public GameObject parent;

    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text contentText;
    [SerializeField] GameObject exampleImage;

    public Book[] book = new Book[17];
    public int pageNumber = 0;
    [SerializeField]int pageCount;

    // Start is called before the first frame update
    void Start()
    {
        pageCount = book.Length-1;
        title.text = book[pageNumber].title;
        contentText.text = book[pageNumber].content;
        exampleImage.GetComponent<Image>().sprite = book[pageNumber].example;
    }

    public void NextPage()
    {
        BookDisplayer bookDisplayer = parent.GetComponent<BookDisplayer>();
        if (bookDisplayer.pageNumber < pageCount)
        {
            bookDisplayer.pageNumber++;
            title.text = book[bookDisplayer.pageNumber].title;
            contentText.text = book[bookDisplayer.pageNumber].content;
            contentText.fontSize = book[bookDisplayer.pageNumber].fontSize;
            exampleImage.GetComponent<Image>().sprite = book[bookDisplayer.pageNumber].example;
        }
        
    }

    public void PreviousPage()
    {
        BookDisplayer bookDisplayer = parent.GetComponent<BookDisplayer>();

        if (bookDisplayer.pageNumber > 0)
        {
            bookDisplayer.pageNumber--;
            title.text = book[bookDisplayer.pageNumber].title;
            contentText.text = book[bookDisplayer.pageNumber].content;
            contentText.fontSize = book[bookDisplayer.pageNumber].fontSize;
            exampleImage.GetComponent<Image>().sprite = book[bookDisplayer.pageNumber].example;

        }
        
    }

}



