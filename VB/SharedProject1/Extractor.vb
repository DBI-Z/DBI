﻿Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.XPath

Public Class Extractor
	Implements IExtractor
	Private displayer As IDisplayer

		Public Sub New(ByVal displayer As IDisplayer)
			Me.displayer = displayer
		End Sub

		Public Function Extract(ByVal xbrl As XDocument) As List(Of WriteFormat) Implements IExtractor.Extract
			Dim ccRecordsIterator As XPathNodeIterator = GetCCIterator(xbrl)
			Dim wf As List(Of WriteFormat) = DeserializeRecords(ccRecordsIterator)
			Return wf
		End Function

		Private Function GetCCIterator(ByVal doc As XDocument) As XPathNodeIterator
			Dim n As XPathNavigator = doc.CreateNavigator()
			Dim m As XmlNamespaceManager = New XmlNamespaceManager(n.NameTable)
			m.AddNamespace("cc", "http://www.ffiec.gov/xbrl/call/concepts")
			Return TryCast(n.Evaluate("//cc:*", m), XPathNodeIterator)
		End Function

		Private Function DeserializeRecords(ByVal ccElements As XPathNodeIterator) As List(Of WriteFormat)
			Dim wf As List(Of WriteFormat) = New List(Of WriteFormat)

			Dim mtBool As Boolean = Nothing

			While ccElements.MoveNext()
				Dim mtContextRef = ccElements.Current.GetAttribute("contextRef", String.Empty)

				If ccElements.Current.MoveToChild(XPathNodeType.Text) Then
					Dim isConceptDataRecord As Boolean = Boolean.TryParse(ccElements.Current.Value, mtBool)

					If isConceptDataRecord Then
						ccElements.Current.MoveToParent()
						displayer.WriteLine(mtContextRef & " -> " & mtBool.ToString())
						wf.Add(New WriteFormat With {
										.MtMdrm = ccElements.Current.LocalName,
										.MtData = If(mtBool, "true", "false"),
										.MtDecimals = "0",
										.mtContextRef = mtContextRef
						})
					Else
						ccElements.Current.MoveToParent()
						Dim mtText As String = ccElements.Current.Value
						Dim mtMdrm As String = ccElements.Current.LocalName
						Dim mtUnitRef As String = ccElements.Current.GetAttribute("unitRef", String.Empty)
						Dim mtDecimals As String = ccElements.Current.GetAttribute("decimals", String.Empty)
						wf.Add(New WriteFormat With {
										.mtDecimals = mtDecimals,
										.MtData = mtText,
										.mtMdrm = mtMdrm,
										.mtUnitRef = mtUnitRef,
										.mtContextRef = mtContextRef
						})
					End If
				Else
					displayer.WriteLine("no text child node?")
				End If
			End While

			Return wf
		End Function
End Class