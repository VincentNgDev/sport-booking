"use client";
import React from "react";
import { Card } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import {
  ResourceSchedulerResource,
  useResourceScheduler,
} from "./resource-scheduler-provider";
import { cn } from "@/lib/utils";

type Selection = {
  resource: ResourceSchedulerResource;
  startIndex: number;
  endIndex: number;
};

type TimeSlot = {
  startTime: { hour: number; minute: number };
  endTime: { hour: number; minute: number };
  display: string; // e.g., "08:00 - 09:00"
};

function resourceTimesToDates(
  resource: ResourceSchedulerResource,
  date: Date = new Date()
) {
  const timings = resource.timings.map((timing) => ({
    startTime: new Date(
      date.getFullYear(),
      date.getMonth(),
      date.getDate(),
      timing.startTime.hour,
      timing.startTime.minute
    ),
    endTime: new Date(
      date.getFullYear(),
      date.getMonth(),
      date.getDate(),
      timing.endTime.hour,
      timing.endTime.minute
    ),
    status: timing.status,
  }));

  return timings.sort((a, b) => a.startTime.getTime() - b.startTime.getTime());
}

function calculateTimeSlots(
  resources: ResourceSchedulerResource[],
  interval: number,
  date: Date = new Date()
) {
  // Get the largest period between start and end times across all resources
  let maxPeriod = 0;
  let minStartTime: Date | null = null;
  resources.forEach((resource) => {
    if (resource.timings.length === 0) return;
    const timings = resourceTimesToDates(resource, date);

    const firstTiming = timings[0];
    const lastTiming = timings[resource.timings.length - 1];

    const period =
      lastTiming.endTime.getTime() - firstTiming.startTime.getTime();
    if (period > maxPeriod) {
      maxPeriod = period;
    }

    // Update minStartTime to the earliest start time across all timings
    if (!minStartTime || firstTiming.startTime < minStartTime) {
      minStartTime = firstTiming.startTime;
    }
  });

  const maxPeriodInMinutes = maxPeriod / 1000 / 60;

  // Calculate the number of slots based on the interval and the maximum period
  const slotCount = Math.ceil(maxPeriodInMinutes / interval);

  // Create an array of time slots
  const timeSlots: TimeSlot[] = [];
  let currentTime = new Date(minStartTime || Date.now());
  for (let i = 0; i < slotCount; i++) {
    const display = `${currentTime.getHours()}:${String(
      currentTime.getMinutes()
    ).padStart(2, "0")}`;
    timeSlots.push({
      startTime: {
        hour: currentTime.getHours(),
        minute: currentTime.getMinutes(),
      },
      endTime: {
        hour: currentTime.getHours(),
        minute: currentTime.getMinutes() + interval,
      },
      display: display,
    });
    currentTime.setMinutes(currentTime.getMinutes() + interval);
  }

  return timeSlots;
}

function checkAvailability(
  resource: ResourceSchedulerResource,
  date: Date,
  timeSlot: TimeSlot
) {
  const timings = resourceTimesToDates(resource, date);

  const periodStart = new Date(
    date.getFullYear(),
    date.getMonth(),
    date.getDate(),
    timeSlot.startTime.hour,
    timeSlot.startTime.minute
  );
  const periodEnd = new Date(
    date.getFullYear(),
    date.getMonth(),
    date.getDate(),
    timeSlot.endTime.hour,
    timeSlot.endTime.minute
  );

  return timings.some((timing) => {
    const slotStart = timing.startTime;
    const slotEnd = timing.endTime;
    return (
      slotStart >= periodStart &&
      slotEnd <= periodEnd &&
      timing.status === "available"
    );
  });
}

type ResourceSchedulerProps = {
  caption?: string;
};

export default function ResourceScheduler({ caption }: ResourceSchedulerProps) {
  const { interval, resources, currentDate } = useResourceScheduler();
  const [periods, setPeriods] = React.useState<TimeSlot[]>([]);

  React.useEffect(() => {
    const timeSlots = calculateTimeSlots(resources, interval, currentDate);
    setPeriods(timeSlots);
  }, [resources, interval, currentDate]);

  const [selection, setSelection] = React.useState<Selection | null>(null);
  const [isMouseDown, setIsMouseDown] = React.useState<boolean>(false);
  const [tempSelection, setTempSelection] = React.useState<Selection | null>(
    null
  );

  const containerRef = React.useRef<HTMLDivElement>(null);

  React.useEffect(() => {
    const handleGlobalClick = (e: MouseEvent) => {
      if (
        containerRef.current &&
        !containerRef.current.contains(e.target as Node)
      ) {
        setSelection(null);
      }
    };
    document.addEventListener("click", handleGlobalClick);
    return () => document.removeEventListener("click", handleGlobalClick);
  }, []);

  const handleMouseDown = React.useCallback(
    (
      resource: ResourceSchedulerResource,
      hourIndex: number,
      e: React.MouseEvent
    ) => {
      e.stopPropagation();
      // If the user clicks on a slot while dragging, finalize the selection
      if (isMouseDown && tempSelection) {
        setIsMouseDown(false);
        setSelection(tempSelection);
        setTempSelection(null);
        return;
      }

      // Start a new selection
      setIsMouseDown(true);
      setTempSelection({
        resource,
        startIndex: hourIndex,
        endIndex: hourIndex,
      });
    },
    [setIsMouseDown, setTempSelection, setSelection, isMouseDown, tempSelection]
  );

  const handleMouseEnter = React.useCallback(
    (resource: ResourceSchedulerResource, hourIndex: number) => {
      if (
        isMouseDown &&
        tempSelection &&
        tempSelection.resource.id === resource.id
      ) {
        setTempSelection((prev) =>
          prev ? { ...prev, endIndex: hourIndex } : null
        );
      }
    },
    [setTempSelection, isMouseDown, tempSelection]
  );

  const handleMouseUp = React.useCallback(() => {
    if (tempSelection) {
      setSelection(tempSelection);
      setTempSelection(null);
    }
    setIsMouseDown(false);
  }, [tempSelection, setSelection, setTempSelection, setIsMouseDown]);

  const isSelected = (
    resource: ResourceSchedulerResource,
    index: number
  ): boolean => {
    const current = tempSelection ?? selection;
    if (!current || current.resource.id !== resource.id) return false;
    const [min, max] = [current.startIndex, current.endIndex].sort(
      (a, b) => a - b
    );
    return index >= min && index <= max;
  };

  return (
    <div className="p-4 w-full" onMouseUp={handleMouseUp}>
      {caption && <h2 className="text-xl font-bold mb-4">{caption}</h2>}
      <div
        ref={containerRef}
        className={cn(
          "grid gap-1 items-center select-none",
          "hover:overflow-x-auto overflow-x-hidden"
        )}
        style={{
          gridTemplateColumns: `minmax(200px, auto) repeat(${periods.length}, minmax(80px, auto))`,
        }}
      >
        <div></div>
        {periods.map((hour, idx) => (
          <div key={idx} className={cn("text-sm font-semibold text-left")}>
            {hour.display.includes("30") ? "" : hour.display}{" "}
            {/* Optional: Add AM/PM if needed */}
          </div>
        ))}
        {resources.map((resource, resIdx) => {
          return (
            <React.Fragment key={resource.id ?? `resource-${resIdx}`}>
              <div className="font-medium text-left pr-2">{resource.name}</div>
              {periods.map((p, hourIdx) => {
                return checkAvailability(resource, currentDate, p) ? (
                  <Card
                    key={hourIdx}
                    className={cn(
                      "h-12 border rounded cursor-pointer",
                      isSelected(resource, hourIdx)
                        ? "bg-primary"
                        : "hover:bg-primary/30"
                    )}
                    onMouseDown={(e) => handleMouseDown(resource, hourIdx, e)}
                    onMouseEnter={() => handleMouseEnter(resource, hourIdx)}
                  />
                ) : (
                  <Card
                    key={hourIdx}
                    className="h-12 border rounded bg-muted"
                  ></Card>
                );
              })}
            </React.Fragment>
          );
        })}
      </div>
    </div>
  );
}
