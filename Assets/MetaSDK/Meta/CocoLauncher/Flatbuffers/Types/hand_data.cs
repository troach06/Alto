// Copyright Â© 2018, Meta Company.  All rights reserved.
// 
// Redistribution and use of this software (the "Software") in binary form, without modification, is 
// permitted provided that the following conditions are met:
// 
// 1.      Redistributions of the unmodified Software in binary form must reproduce the above 
//         copyright notice, this list of conditions and the following disclaimer in the 
//         documentation and/or other materials provided with the distribution.
// 2.      The name of Meta Company (â€œMetaâ€) may not be used to endorse or promote products derived 
//         from this Software without specific prior written permission from Meta.
// 3.      LIMITATION TO META PLATFORM: Use of the Software is limited to use on or in connection 
//         with Meta-branded devices or Meta-branded software development kits.  For example, a bona 
//         fide recipient of the Software may incorporate an unmodified binary version of the 
//         Software into an application limited to use on or in connection with a Meta-branded 
//         device, while he or she may not incorporate an unmodified binary version of the Software 
//         into an application designed or offered for use on a non-Meta-branded device.
// 
// For the sake of clarity, the Software may not be redistributed under any circumstances in source 
// code form, or in the form of modified binary code â€“ and nothing in this License shall be construed 
// to permit such redistribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL META COMPANY BE LIABLE FOR ANY DIRECT, 
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace meta.types
{

using global::System;
using global::FlatBuffers;

public enum HandType : int
{
 RIGHT = 0,
 LEFT = 1,
};

public struct Vec3T : IFlatbufferObject
{
  private Struct __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public Vec3T __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public float X { get { return __p.bb.GetFloat(__p.bb_pos + 0); } }
  public float Y { get { return __p.bb.GetFloat(__p.bb_pos + 4); } }
  public float Z { get { return __p.bb.GetFloat(__p.bb_pos + 8); } }

  public static Offset<Vec3T> CreateVec3T(FlatBufferBuilder builder, float X, float Y, float Z) {
    builder.Prep(4, 12);
    builder.PutFloat(Z);
    builder.PutFloat(Y);
    builder.PutFloat(X);
    return new Offset<Vec3T>(builder.Offset);
  }
};

public struct HandData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static HandData GetRootAsHandData(ByteBuffer _bb) { return GetRootAsHandData(_bb, new HandData()); }
  public static HandData GetRootAsHandData(ByteBuffer _bb, HandData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public HandData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int HandId { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public HandType HandType { get { int o = __p.__offset(6); return o != 0 ? (HandType)__p.bb.GetInt(o + __p.bb_pos) : HandType.RIGHT; } }
  public Vec3T? Top { get { int o = __p.__offset(8); return o != 0 ? (Vec3T?)(new Vec3T()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Vec3T? Palm { get { int o = __p.__offset(10); return o != 0 ? (Vec3T?)(new Vec3T()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Vec3T? HandAnchor { get { int o = __p.__offset(12); return o != 0 ? (Vec3T?)(new Vec3T()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public Vec3T? GrabAnchor { get { int o = __p.__offset(14); return o != 0 ? (Vec3T?)(new Vec3T()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public bool IsGrabbing { get { int o = __p.__offset(16); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static void StartHandData(FlatBufferBuilder builder) { builder.StartObject(7); }
  public static void AddHandId(FlatBufferBuilder builder, int handId) { builder.AddInt(0, handId, 0); }
  public static void AddHandType(FlatBufferBuilder builder, HandType handType) { builder.AddInt(1, (int)handType, 0); }
  public static void AddTop(FlatBufferBuilder builder, Offset<Vec3T> topOffset) { builder.AddStruct(2, topOffset.Value, 0); }
  public static void AddPalm(FlatBufferBuilder builder, Offset<Vec3T> palmOffset) { builder.AddStruct(3, palmOffset.Value, 0); }
  public static void AddHandAnchor(FlatBufferBuilder builder, Offset<Vec3T> handAnchorOffset) { builder.AddStruct(4, handAnchorOffset.Value, 0); }
  public static void AddGrabAnchor(FlatBufferBuilder builder, Offset<Vec3T> grabAnchorOffset) { builder.AddStruct(5, grabAnchorOffset.Value, 0); }
  public static void AddIsGrabbing(FlatBufferBuilder builder, bool isGrabbing) { builder.AddBool(6, isGrabbing, false); }
  public static Offset<HandData> EndHandData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<HandData>(o);
  }
  public static void FinishHandDataBuffer(FlatBufferBuilder builder, Offset<HandData> offset) { builder.Finish(offset.Value); }
};


}
