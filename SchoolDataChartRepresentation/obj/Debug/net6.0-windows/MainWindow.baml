   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5   	   X TSchoolDataChartRepresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:SchoolDataChartRepresentationSchoolDataChartRepresentation 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   .   (SchoolDataChartRepresentation.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	   =local+clr-namespace:SchoolDataChartRepresentation     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$  
MainWindow��5
   	   $	��450��6   $	��900��6)    � 	Resources 5      3 -SchoolDataChartRepresentation.ComboBoxManager   ComboBoxManagerInstance��%   &	          5   
   6    5      .��5      � ���	#FF262626 � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��5* �6    � ColumnDefinitions 5   
   �� 5      $��4* �6   �� 5      $��2* �6   .+�5   
   #   LiveCharts.Wpf.CartesianChart  ���#FFFFFF&    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�6V      Zoom$
 None=�6m   $��2��6y      Series 6   �� *SeriesCollection+   AxisX 5         LiveCharts.Wpf.Axis     Labels 6   �� *Labels+67   5      �� 5   
   -   5        	 ��Name$	 cb_Category��5      $y�Hidden=�6(   $��1��6<   $��Center=�6L   $��	0,168,0,0q�6i   $	��Top=�6|   $	��120��6�   $��1��6�  ��6�   �� *ComboBoxItems+
 �� Source#
 [   �� SelectedValue 6�   �� *SelectedItem+#
 [   �� Mode$ TwoWay=�6�  �� 5   
   -   5         ��Name$ 
l_Category��5       $��1��6   $y�Hidden=�64   $��Wybierz porównanie��6H   $��	0,140,0,0q�6e   $��Center=�6x   $	��Top=�6�   ���#FFFFFF$��1��6�   �� 5   
   -   5         ��Name$ CheckboxList��5      $��1��6(   $y�Hidden=�6c   ��5      �� 5       .R�5!      ��  �� Orientation$ 
Horizontal=�6!   .+�5"      �� -   5        ��5"   #   ��  d� 	IsChecked 5%   #   �� *	IsChecked+$ TwoWay=�5&      5'      5(      5*      � 5+   
   $��Right=�6   $��Bottom=�6+   $��10q�6F   $��1��6R   $��1��6b    5,      �� 5-      $
��Auto �6#   �� 5.      $
��Auto �6#   �� 5/      $
��Auto �6#    51      �� 52      $
��Auto �6    �� 53      $
��Auto �6    .+�56      �� $��	Username:��6   
�������$��0,0,5,0q�6<   �� 57      -   5         ��Name$ UsernameTextBox��57   &   $��1��6   $	��120��6?   �� 59      $��1��6   $��	Password:��6%   
�������$��0,5,5,0q�6I   G� 5:      -   5         G�Name$ PasswordBox��5:   7   $��1��6   $��1��6'   $	��120��6L   �� 5<      -   5        $��1��5<      $��2��6"   $��Login��62   $��80��6B   6M   5=      �� 5>   
   -   5         ��Name$ consoleOutput��5>      $��2��6+   $��1��6<   $��0,124,0,241q�6L   5?      5@      