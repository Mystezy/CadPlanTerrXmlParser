   M S B A M L   `   `   `  ????O  KCadPlanTerrXmlParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:CadPlanTerrXmlParserCadPlanTerrXmlParser  5   	   %    CadPlanTerrXmlParser.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:CadPlanTerrXmlParser     5   	     ? Title$  
Visualizer??5   	   $	??450??6   $	??800??6)    ? 	Resources 5	         TreeViewStyle ?? 
TargetType ?? Property ?? Value ?? Mode-  'System.Windows.Input.KeyboardNavigation  CheckBoxTemplate ?? ItemsSource ?? Orientation d? 	IsChecked?? 5
   
   %  &	        &	 ?     ?? 5      " Q?6*   .?5      ??  ?@Q?
IsExpanded .  6/   ?? 5       ?@Q?
IsSelected 6/   ?? *IsInitiallySelected+$ OneTime=??? 5       ?@ AcceptsReturn . 6E   5      ?? 5       6@   ?? *
Children+$ OneTime=?.R?5      ?? $ 
Horizontal=?6   $??0q?67   .+?5      ?? |?.  $??Center=?6Q   $??0q?6l    61   ?? *	IsChecked+6l   ?? 5      -   5        $	??2pxq?5   C   ??6   ?? *Name+$ OneTime=?6Q   5      5      5      .??5      ? -   5        	 ? RowDefinitions	 5   
   ?? 5      $
??0.1* ?6   ?? 5      $??* ?6   ?? 5      $
??0.1* ?6   ?? 5      $
??0.1* ?6   
 ? ColumnDefinitions
 5   
   ?? 5       $??0.75* ?6   ?? 5!      $??0.05* ?6   ?? 5"      $??* ?6   .+?5$   
   ?? $??0??6   $??0??6   $#??Дерево объектов??6-   $??Bottom=?6H   $
??20,0q?6c   ?? 5%   
   $??0??6   $??2??6   $??Отображение??6.   $??Bottom=?6E   $
??10,0q?6`   R? 5&   
   -   5         R?Name$ treeElementsView??5&   A   $??1??6   $??0??6    $??2??60   $??15, 0 ,0, 0q?6\   #??[  5'      #??[ 5(      ?? 5)   
   -   5        $??DataTextBox??5)   /   $??1??6   $??2??6    ?? TextWrapping$
 Wrap=?6C   $??0, 0, 15, 0q?6W   $
??Auto=?6l    ?? 
IsReadOnly . 6?   ?? 5*   
   -   5        $??2??5*      $??2??6   $??2??6/   $?? Сохранить??6@   $	??Top=?6U   $??Center=?6m   $??0,17,0,0q?6?   $??16(?6?   6?   5+      5,      