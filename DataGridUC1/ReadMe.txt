

This project is based on the MS Learn example..

I've added the following features

- Restructuring of the MS Learn example to MVVM concept
- Read/Write data from/to an xml file with a strongly typed MainDataSet.xsd
- Expander for RowDetails area, the DataGrid could be set to ReadOnly because we can edit data in the RowDetails area.
- Test Button for fire the related hyperlink on the RowDetails area
- Simple Text Search
- Add New Row button

The method that the MS Learn example uses to fileter checked/unchecked tasks, inspired me to create the following super easy Text Search.
With the FilterEventArgs we get the DataRowView for each task/row what allows us to use simple if statements for this method.

   RichTextColumn and RichTextBoxFormatBar
   https://github.com/xceedsoftware/wpftoolkit
   Starting at v4.0.0, this free toolkit is provided under the Xceed Community License agreement (for non-commercial use).
   https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md
