using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VMFramework.Core
{
    public sealed partial class CircularSelectChooser<TItem> : IChooser<TItem>
    {
        public bool pingPong { get; }
        
        public int startCircularIndex { get; }

        public int currentCircularIndex { get; private set; }

        public int currentCircularTimes { get; private set; } = 1;

        public bool loopForward { get; private set; } = true;

        private readonly CircularSelectItem<TItem>[] items;

        public IReadOnlyList<CircularSelectItem<TItem>> circularItems => items;

        public CircularSelectChooser(CircularSelectItem<TItem>[] items, bool pingPong = false,
            int startCircularIndex = 0)
        {
            this.items = items;
            this.pingPong = pingPong;

            if (startCircularIndex >= items.Length)
            {
                Debug.LogWarning(
                    $"{nameof(startCircularIndex)} : {startCircularIndex} is greater than or equal to " +
                    $"the number of items in the {nameof(CircularSelectChooser<TItem>)}!");
            }

            this.startCircularIndex = startCircularIndex.Clamp(0, items.Length - 1);
            currentCircularIndex = this.startCircularIndex;

            if (this.items.Length == 0)
            {
                Debug.LogError($"{nameof(CircularSelectChooser<TItem>)} has no items!");
                return;
            }

            if (this.items.Length == 1 && pingPong)
            {
                Debug.LogWarning(
                    $"{nameof(CircularSelectChooser<TItem>)} has only one item and ping-pong is enabled!");
            }
        }

        public CircularSelectChooser(IEnumerable<CircularSelectItem<TItem>> items, bool pingPong = false,
            int startCircularIndex = 0) : this(items.ToArray(), pingPong, startCircularIndex)
        {
        }

        public CircularSelectChooser(IEnumerable<TItem> items, bool pingPong = false, int startCircularIndex = 0)
            : this(items.Select(item => new CircularSelectItem<TItem>(item)), pingPong, startCircularIndex)
        {
        }

        public CircularSelectChooser(IEnumerable<ICircularSelectItem<TItem>> items, bool pingPong = false,
            int startCircularIndex = 0) : this(
            items.Select(item => new CircularSelectItem<TItem>(item.value, item.times)), pingPong,
            startCircularIndex)
        {
        }

        public void ResetChooser()
        {
            currentCircularIndex = startCircularIndex;
            currentCircularTimes = 1;
            loopForward = true;
        }

        public TItem GetValue()
        {
            if (items.Length == 0)
            {
                return default;
            }

            var item = items[currentCircularIndex];

            if (pingPong == false)
            {
                currentCircularTimes++;
                if (currentCircularTimes > item.times)
                {
                    currentCircularTimes = 1;
                    currentCircularIndex++;

                    if (currentCircularIndex >= items.Length)
                    {
                        currentCircularIndex = 0;
                    }
                }
            }
            else
            {
                currentCircularTimes++;
                if (currentCircularTimes > item.times)
                {
                    currentCircularTimes = 1;

                    if (loopForward)
                    {
                        currentCircularIndex++;

                        if (currentCircularIndex >= items.Length)
                        {
                            currentCircularIndex = items.Length - 2;
                            loopForward = false;
                        }
                    }
                    else
                    {
                        if (currentCircularIndex <= 0)
                        {
                            currentCircularIndex++;
                            loopForward = true;
                        }
                        else
                        {
                            currentCircularIndex--;
                        }
                    }
                }
            }

            return item.value;
        }
    }
}