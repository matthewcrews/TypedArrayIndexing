
;Scratchpad.BlockOrdered(Block`1<Int32>)
L0000: xor eax, eax
L0002: xor edx, edx
L0004: mov r8d, [rcx+8]
L0008: dec r8d
L000b: cmp r8d, edx
L000e: jle short L001b
L0010: movsxd rax, edx
L0013: mov eax, [rcx+rax*4+0x10]
L0017: inc edx
L0019: jmp short L000b
L001b: ret

;Scratchpad.ArrayOrdered(Int32[])
L0000: xor eax, eax
L0002: xor edx, edx
L0004: mov r8d, [rcx+8]
L0008: dec r8d
L000b: cmp r8d, edx
L000e: jle short L001b
L0010: movsxd rax, edx
L0013: mov eax, [rcx+rax*4+0x10]
L0017: inc edx
L0019: jmp short L000b
L001b: ret

Scratchpad+Block`1..ctor(!0[])
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.get_Values()
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.CompareTo(Block`1<!0>)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.CompareTo(System.Object)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.CompareTo(System.Object, System.Collections.IComparer)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.GetHashCode(System.Collections.IEqualityComparer)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.GetHashCode()
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.Equals(System.Object, System.Collections.IEqualityComparer)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.get_Item(Int32)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.set_Item(Int32, !0)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.get_Length()
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.Equals(Block`1<!0>)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.Equals(System.Object)
; generic method cannot be jitted. provide explicit types

;System.ValueType.ToString()
L0000: sub rsp, 0x28
L0004: call qword ptr [Internal.Runtime.CompilerServices.Unsafe.IsNullRef[[System.__Canon, System.Private.CoreLib]](System.__Canon ByRef)]
L000a: cmp [rax], eax
L000c: mov rcx, rax
L000f: mov edx, 1
L0014: call qword ptr [Internal.Runtime.CompilerServices.Unsafe.IsNullRef[[System.__Canon, System.Private.CoreLib]](System.__Canon ByRef)]
L001a: nop
L001b: add rsp, 0x28
L001f: ret

Scratchpad+Block`1..ctor(!0[])
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.get_Values()
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.CompareTo(Block`1<!0>)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.CompareTo(System.Object)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.CompareTo(System.Object, System.Collections.IComparer)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.GetHashCode(System.Collections.IEqualityComparer)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.GetHashCode()
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.Equals(System.Object, System.Collections.IEqualityComparer)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.get_Item(Int32)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.set_Item(Int32, !0)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.get_Length()
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.Equals(Block`1<!0>)
; generic method cannot be jitted. provide explicit types

Scratchpad+Block`1.Equals(System.Object)
; generic method cannot be jitted. provide explicit types

;System.ValueType.ToString()
L0000: sub rsp, 0x28
L0004: call qword ptr [Internal.Runtime.CompilerServices.Unsafe.IsNullRef[[System.__Canon, System.Private.CoreLib]](System.__Canon ByRef)]
L000a: cmp [rax], eax
L000c: mov rcx, rax
L000f: mov edx, 1
L0014: call qword ptr [Internal.Runtime.CompilerServices.Unsafe.IsNullRef[[System.__Canon, System.Private.CoreLib]](System.__Canon ByRef)]
L001a: nop
L001b: add rsp, 0x28
L001f: ret
