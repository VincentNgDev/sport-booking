import React from "react";

type ResourceSchedulerContext = {
  resources: ResourceSchedulerResource[];
  interval: number; // in minutes
  currentDate: Date; // Optional current date for scheduling
  handleDateChange: (date: Date) => void;
};

const resourceSchedulerContext = React.createContext<
  ResourceSchedulerContext | undefined
>(undefined);

export function useResourceScheduler() {
  const context = React.useContext(resourceSchedulerContext);
  if (!context) {
    throw new Error(
      "useResourceScheduler must be used within a ResourceSchedulerProvider"
    );
  }
  return context;
}

type ResourceSchedulerProviderProps = {
  children?: React.ReactNode;
  resources: ResourceSchedulerResource[];
  interval: 30 | 60; // in minutes
  date?: Date; // Optional current date for scheduling
};

function resourcesValidation(
  resources: ResourceSchedulerResource[],
  date: Date
) {
  if (!Array.isArray(resources)) {
    throw new Error("resources must be an array");
  }
  resources.forEach((resource) => {
    if (!resource.id || !resource.name || !Array.isArray(resource.timings)) {
      throw new Error("Each resource must have id, name, and timings");
    }
    const timingsSet = new Set();
    resource.timings.forEach((timing) => {
      const startTime = new Date(
        date.getFullYear(),
        date.getMonth(),
        date.getDate(),
        timing.startTime.hour,
        timing.startTime.minute
      );
      const endTime = new Date(
        date.getFullYear(),
        date.getMonth(),
        date.getDate(),
        timing.endTime.hour,
        timing.endTime.minute
      );

      if (startTime > endTime) {
        throw new Error(
          `Timing start time must be before end time for resource ${
            resource.name
          } at ${JSON.stringify(timing)}`
        );
      }

      const timingKey = `${startTime}-${endTime}`;
      if (timingsSet.has(timingKey)) {
        throw new Error(
          `Timings overlap detected for resource ${
            resource.name
          } at ${JSON.stringify(timing)}`
        );
      }
      timingsSet.add(timingKey);
    });
  });
}

export function ResourceSchedulerProvider({
  children,
  resources,
  interval,
  date,
}: ResourceSchedulerProviderProps) {
  const [currentDate, setCurrentDate] = React.useState<Date>(
    date ?? new Date()
  );

  resourcesValidation(resources, currentDate);

  const handleDateChange = React.useCallback(
    (date: Date) => {
      setCurrentDate(date);
    },
    [setCurrentDate]
  );

  const context = React.useMemo(
    () => ({ resources, interval, currentDate, handleDateChange }),
    [resources, interval, currentDate, handleDateChange]
  );

  return (
    <resourceSchedulerContext.Provider value={context}>
      {children}
    </resourceSchedulerContext.Provider>
  );
}

export interface ResourceSchedulerTiming {
  startTime: ResourceTime;
  endTime: ResourceTime;
  status: "available" | "unavailable";
  [key: string]: any; // Allow additional properties
}

export interface ResourceSchedulerResource {
  id: string;
  name: string;
  timings: ResourceSchedulerTiming[];
  [key: string]: any; // Allow additional properties
}

interface ResourceTime {
  hour: number;
  minute: number;
  [key: string]: any; // Allow additional properties
}
