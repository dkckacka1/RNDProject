using System;
using System.Text;
using UnityEngine;

public class SerializeReferenceLinkedListExample : MonoBehaviour
{
    // This example shows a linked list structure with a single int per Node.
    // This would be much more efficiently represented using a List<int>, without any SerializeReference needed.
    // But it demonstrates an approach that can be extended for trees and other more advanced graphs

    [Serializable]
    public class Node
    {
        // This field must use serialize reference so that serialization can store
        // a reference to another Node object, or null.  By-value
        // can never properly represent this sort of self-referencing structure.
        [SerializeReference]
        public Node m_Next = null;

        public int m_Data = 1;
    }

    [SerializeReference]
    public Node m_Front = null;

    // Points to the last node in the list.  This is an
    // example of a having more than one field pointing to a single Node
    // object, which cannot be done with "by-value" serialization
    [SerializeReference]
    public Node m_End = null;

    SerializeReferenceLinkedListExample()
    {
        AddEntry(1);
        AddEntry(3);
        AddEntry(9);
        AddEntry(81);
        PrintList();
    }

    private void AddEntry(int data)
    {
        if (m_Front == null)
        {
            m_Front = new Node() { m_Data = data };
            m_End = m_Front;
        }
        else
        {
            m_End.m_Next = new Node() { m_Data = data };
            m_End = m_End.m_Next;
        }
    }

    private void PrintList()
    {
        var sb = new StringBuilder();
        sb.Append("Link list contents: ");
        var position = m_Front;
        while (position != null)
        {
            sb.Append("  Node data " + position.m_Data).AppendLine();
            position = position.m_Next;
        }
        Debug.Log(sb.ToString());
    }
}