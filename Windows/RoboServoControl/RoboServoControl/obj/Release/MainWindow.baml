   M S B A M L   `   `   `  ���� K  GRoboServoControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:RoboServoControlRoboServoControl  !    RoboServoControl.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:RoboServoControl       � Title$  RoboServoControl v1.0��$��222.201��$	��525��.��� .+��� -    ��Name$ 
updownsend��$
��Send��$
��Left=�$��
19,102,0,0q�$	��Top=�$��75���� -    ��Name$ 	updownval��$
��Left=�$��23��$��	18,74,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��76���� -    ��Name$ groupBox��$��UPDOWN��$
��Left=�$��	10,56,0,0q�$	��Top=�$	��102��$��93��.���� -    ��Name$ label1_Copy��$��+ runter | - rauf��$
��Left=�$��28��$��
0,54,-2,-2q�$	��Top=�$��83��$��10(��� -   $ label��$��IP-Adresse:��$
��Left=�$��	10,10,0,0q�$	��Top=��� -   $ ip��$
��Left=�$��23��$��	84,14,0,0q�$
 Wrap=�$	��Top=�$	��120��$��10.99.13.37���� -   $ connect��$��Connect��$
��Left=�$��
209,16,0,0q�$	��Top=�$��75���� -   $ forwardsend��$
��Send��$
��Left=�$��120,102,0,0q�$	��Top=�$��75���� -	   $ 
forwardval��$
��Left=�$��23��$��
119,74,0,0q�$
 Wrap=�$	��Top=�$��76���� -
   $ groupBox_Copy��$��FORWARD��$
��Left=�$��
111,56,0,0q�$	��Top=�$	��102��$��93��.���� -   $ label2��$��+ hinter | - vor��$
��Left=�$��28��$��
0,54,-2,-2q�$	��Top=�$��83��$��10(��� -   $ schwenksend��$
��Send��$
��Left=�$��218,102,0,0q�$	��Top=�$��75���� -   $ 
schwenkval��$
��Left=�$��23��$��
217,74,0,0q�$
 Wrap=�$	��Top=�$��76���� -   $ groupBox_Copy1��$��SCHWENK��$
��Left=�$��
209,56,0,0q�$	��Top=�$	��102��$��93��.���� -   $ label3��$��+ rechts | - links��$
��Left=�$��28��$��
0,54,-2,-2q�$	��Top=�$��83��$��10(��� -   $ 	greifsend��$
��Send��$
��Left=�$��316,102,0,0q�$	��Top=�$��75���� -   $ greifval��$
��Left=�$��23��$��
315,74,0,0q�$
 Wrap=�$	��Top=�$��76���� -   $ groupBox_Copy2��$��GSCHWENK��$
��Left=�$��
307,56,0,0q�$	��Top=�$	��102��$��93���� -   $ greifersend��$
��Send��$
��Left=�$��414,102,0,0q�$	��Top=�$��75���� -   $ 
greiferval��$
��Left=�$��23��$��
413,74,0,0q�$
 Wrap=�$	��Top=�$��76���� -   $ groupBox_Copy3��$��GREIF��$
��Left=�$��
405,56,0,0q�$	��Top=�$	��102��$��93���� -   $ 
disconnect��$��
Disconnect��$
��Left=�$��
289,16,0,0q�$	��Top=�$��75���� -   $ 
greiferauf��$	��Auf��$
��Left=�$��414,127,0,0q�$	��Top=�$��34���� -   $ 	greiferzu��$��Zu��$
��Left=�$��453,127,0,0q�$	��Top=�$��36���� -   $
 idle��$
��IDLE��$
��Left=�$��
19,163,0,0q�$��75��$��19��$	��Top=��� -   $ kugel��$��KUGEL��$
��Left=�$��
99,163,0,0q�$	��Top=�$��75���� -   $ links��$��LINKS��$
��Left=�$��179,163,0,0q�$	��Top=�$��75���� -   $ rechts��$��RECHTS��$
��Left=�$��259,163,0,0q�$	��Top=�$��75���� -   $
 work��$
��WORK��$
��Left=�$��423,163,0,0q�$	��Top=�$��75���� -   $
 kill��$
��KILL��$
��Left=�$��339,163,0,0q�$	��Top=�$��75��