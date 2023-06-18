
/*===================================================================================
* 
*   Copyright (c) Userware/OpenSilver.net
*      
*   This file is part of the OpenSilver Runtime (https://opensilver.net), which is
*   licensed under the MIT license: https://opensource.org/licenses/MIT
*   
*   As stated in the MIT license, "the above copyright notice and this permission
*   notice shall be included in all copies or substantial portions of the Software."
*  
\*====================================================================================*/

using System;
using System.ComponentModel;
using OpenSilver.Internal;

#if MIGRATION
namespace System.Windows
#else
namespace Windows.UI.Xaml
#endif
{
    public enum WhenToCallPropertyChangedEnum
    {
        Never = 0,
        IfPropertyIsSet = 1,
        [Obsolete(Helper.ObsoleteMemberMessage)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        Always = 2,
    }
}
