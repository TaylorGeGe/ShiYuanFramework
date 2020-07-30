using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sy
{
    public static class Extensions
    {
        public static T PositionX<T>(this T self, float x) where T : Component
        {

            var transform = self.transform;

            var position = transform.position;

            position.x = x;
            transform.position = position;
            return self;
        }
        public static T PositionY<T>(this T self, float y) where T : Component
        {
            var transform = self.transform;

            var position = transform.position;

            position.y = y;

            transform.position = position;

            return self;
        }


        public static T PositionZ<T>(this T self, float z) where T : Component
        {
            var transform = self.transform;

            var position = transform.position;

            position.z = z;

            transform.position = position;

            return self;
        }

        public static T PositionXY<T>(this T self, float x, float y) where T : Component
        {
            var transform = self.transform;

            var position = transform.position;

            position.x = x;
            position.y = y;

            transform.position = position;

            return self;
        }

        public static T LocalPositionX<T>(this T self, float localPosX) where T : Component
        {
            var transform = self.transform;

            var localPosition = transform.localPosition;

            localPosition.x = localPosX;

            transform.localPosition = localPosition;

            return self;
        }

        public static T LocalPositionY<T>(this T self, float localPosY) where T : Component
        {
            var transform = self.transform;

            var localPosition = transform.localPosition;

            localPosition.y = localPosY;

            transform.localPosition = localPosition;

            return self;
        }

        public static T LocalPositionZ<T>(this T self, float localPosZ) where T : Component
        {
            var transform = self.transform;

            var localPosition = transform.localPosition;

            localPosition.z = localPosZ;

            transform.localPosition = localPosition;

            return self;
        }

        public static T LocalPositionXY<T>(this T self, float localPosX, float localPosY) where T : Component
        {
            var transform = self.transform;

            var localPosition = transform.localPosition;

            localPosition.x = localPosX;
            localPosition.y = localPosY;

            transform.localPosition = localPosition;

            return self;
        }
        public static T LocalIdentity<T>(this T self) where T : Component
        {
            var transform = self.transform;

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            return self;
        }

        public static T Identity<T>(this T self) where T : Component
        {
            var transform = self.transform;

            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            return self;
        }
        public static T Show<T>(this T self) where T : Component
        {
            self.gameObject.SetActive(true);
            return self;
        }

        public static T Hide<T>(this T self) where T : Component
        {
            self.gameObject.SetActive(false);
            return self;
        }

        public static GameObject Show(this GameObject self)
        {
            self.SetActive(true);
            return self;
        }

        public static GameObject Hide(this GameObject self)
        {
            self.SetActive(false);
            return self;
        }
    }
}