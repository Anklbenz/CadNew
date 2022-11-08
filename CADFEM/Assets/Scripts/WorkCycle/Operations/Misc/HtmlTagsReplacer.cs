using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HtmlTagsReplacer {

    private const string P_OPEN_TAG = "<p>"; 
    private const string P_CLOSE_TAG = "</p>"; 
    
    public string ReplaceTagP(string html){
        var result = html.Replace(P_OPEN_TAG, "");
        return result.Replace(P_CLOSE_TAG, "\n");
    }
    
}
