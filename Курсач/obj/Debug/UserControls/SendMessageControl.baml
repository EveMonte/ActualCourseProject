   M S B A M L   `   `   `  ����G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:КурсачКурсач  5      %    Курсач.SendMessageControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Курсач     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5	      � $��Stretch=�6      	GridStyle#���   5
        � RowDefinitions  5   
   �� 5      $��1* �6   �� 5      $
��0.8* �6   �� 5      $��1* �6   �� 5      $��1.25* �6   �� 5      $
��0.7* �6   �� 5      $
��0.7* �6   .+�5   
   �� $��0��6   $
��Wrap=�5      $��16(�5      $
��Left=�5      $��Bottom=�5      $��15, 0, 15, 0q�5        m_SentMessage#���  5      �� 5   
   $��1��6   $
��Wrap=�5      $��16(�5      $��15, 0, 15, 0q�5      $
��Left=�5      $��Bottom=�5        m_ConfirmAccount#���  5       �� 5!   
   -   5         ��Name$ codeBox��5#      $��2��5!      $
��Auto��5$      $��50��5%      $
��18,0q�5&        m_EnterConfirmationCode)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5'       % !MaterialDesignFloatingHintTextBox#��[ 5(      $��16(�5)      ��5"      ��  �� Path$
 Code�.��5)       ��5*      �� 5+       �� 
TargetType" ��6&   .R�5,      �� ��6"   �� $
 Code� �� Mode$ TwoWay=�5-      .��5.   $   5/      �� 50      -   5         ��Name$ Check��52      $��3��50        LightButton#���  51      $
��Auto��53      $
��15 7q�55        m_ConfirmCode#���  56      $��20(�57      $��Stretch=�58      ��54      �� $ SendMessageCommand�58      �� 59   
   $��4��6   
������ $��0q�5;      
������ ��5=      �� *SendNewMessageCommand+.��5>      ��  � Cursor$
 Hand��6   $��Center=�5?        
ResendCode#���  5@      $��16(�5A      ���	#FF1877F35B      5C      �� 5D   
   $��5��6   $��0q�5E      
������ 
������ ��5H      �� *OpenRegistrationCommand+.��5I      �� $
 Hand��6   $��Center=�5J       
 m_Back#���  5K      $��16(�5L      ���	#FF1877F35M      5N      5O      5P      