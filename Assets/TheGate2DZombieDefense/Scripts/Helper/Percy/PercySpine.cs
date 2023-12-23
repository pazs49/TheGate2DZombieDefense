using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public static class PercySpine
{
    //This static class works only for skeleton mecanim!
    public static Vector3 GetPointAttachmentToUnityPosition(SkeletonMecanim skeletonMecanim, string slotName, string attachmentName)
    {
        Attachment attachment;
        attachment = skeletonMecanim.Skeleton.GetAttachment(slotName, attachmentName);

        PointAttachment pointAttachment;
        pointAttachment = attachment as PointAttachment;

        Slot slot;
        slot = skeletonMecanim.Skeleton.FindSlot(slotName);

        Vector3 unityWorldPosition;
        unityWorldPosition = pointAttachment.GetWorldPosition(slot, skeletonMecanim.transform);

        return unityWorldPosition;
    }

    public static float GetPointAttachmentToUnityRotation(SkeletonMecanim skeletonMecanim, string slotName, string attachmentName)
    {
        Attachment attachment;
        attachment = skeletonMecanim.Skeleton.GetAttachment(slotName, attachmentName);

        PointAttachment pointAttachment;
        pointAttachment = attachment as PointAttachment;

        Slot slot;
        slot = skeletonMecanim.Skeleton.FindSlot(slotName);

        float rotation = pointAttachment.ComputeWorldRotation(slot.Bone) + skeletonMecanim.transform.rotation.eulerAngles.z;

        return rotation;
    }
}
