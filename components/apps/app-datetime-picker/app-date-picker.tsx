"use client";

import { Button, buttonVariants } from "@/components/ui/button";
import { Card } from "@/components/ui/card";
import { cn } from "@/lib/utils";
import { ChevronLeft, ChevronRight } from "lucide-react";
import React from "react";

const months = [
  "Jan",
  "Feb",
  "Mar",
  "Apr",
  "May",
  "Jun",
  "Jul",
  "Aug",
  "Sep",
  "Oct",
  "Nov",
  "Dec",
];

const weekdays = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

function getDaysInMonth(year: number, month: number) {
  return new Date(year, month + 1, 0).getDate();
}

function getStartDay(year: number, month: number) {
  return new Date(year, month, 1).getDay();
}

type DatePickerProps = {
  selectedDate?: Date | null;
  onSelect?: (date: Date) => void;
};

const AppDatePicker: React.FC<DatePickerProps> = ({
  onSelect,
  selectedDate,
}) => {
  const [show, setShow] = React.useState(false);
  const [viewMode, setViewMode] = React.useState<"date" | "month" | "year">(
    "date"
  );
  const [selected, setSelected] = React.useState<Date>(
    selectedDate ?? new Date()
  );

  const [currentYearMonthView, setCurrentYearMonthView] = React.useState<
    [number, number]
  >([selected.getFullYear(), selected.getMonth()]);

  const pickerRef = React.useRef<HTMLDivElement>(null);

  React.useEffect(() => {
    const handleClickOutside = (e: MouseEvent) => {
      if (pickerRef.current && !pickerRef.current.contains(e.target as Node)) {
        setShow(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  const selectDate = React.useCallback(
    (d: number) => {
      const date = new Date(
        currentYearMonthView[0],
        currentYearMonthView[1],
        d
      );
      setSelected(date);
      setShow(false);
      onSelect?.(date);
    },
    [
      setSelected,
      setShow,
      onSelect,
      currentYearMonthView,
      setCurrentYearMonthView,
    ]
  );

  const handleChevronOnClick = (direction: "left" | "right") => {
    if (viewMode === "date") {
      setCurrentYearMonthView((prev) => {
        const newMonth = prev[1] + (direction === "left" ? -1 : 1);
        const newYear =
          newMonth < 0 ? prev[0] - 1 : newMonth > 11 ? prev[0] + 1 : prev[0];
        return [newYear, (newMonth + 12) % 12];
      });
    } else if (viewMode === "month") {
      setCurrentYearMonthView((prev) => {
        const newYear = prev[0] + (direction === "left" ? -1 : 1);
        return [newYear, prev[1]];
      });
    } else if (viewMode === "year") {
      setCurrentYearMonthView((prev) => {
        const newYear = prev[0] + (direction === "left" ? -25 : 25);
        return [newYear, prev[1]];
      });
    }
  };

  const renderCalendar = () => {
    const days = getDaysInMonth(
      currentYearMonthView[0],
      currentYearMonthView[1]
    );
    const startDay = getStartDay(
      currentYearMonthView[0],
      currentYearMonthView[1]
    );
    const cells = [];
    // Render day headers
    for (let i = 0; i < weekdays.length; i++)
      cells.push(
        <div
          key={`weekday-${i}`}
          className={cn("w-8 h-8 text-[10px] flex justify-center items-center")}
        >
          {weekdays[i]}
        </div>
      );
    for (let i = 0; i < startDay; i++)
      cells.push(<div key={`blank-${i}`} className="w-8 h-8" />);
    for (let d = 1; d <= days; d++) {
      const isSelected =
        selected &&
        selected.getFullYear() === currentYearMonthView[0] &&
        selected.getMonth() === currentYearMonthView[1] &&
        selected.getDate() === d;
      cells.push(
        <Button
          key={d}
          onClick={() => {
            selectDate(d);
          }}
          variant="ghost"
          className={cn(
            "w-8 h-8 text-sm",
            isSelected
              ? "bg-primary text-primary-foreground hover:bg-primary hover:text-primary-foreground dark:hover:bg-primary dark:hover:text-primary-foreground"
              : ""
          )}
        >
          {d}
        </Button>
      );
    }
    return <div className="grid grid-cols-7 gap-1 mt-2">{cells}</div>;
  };

  const renderMonthSelector = () => (
    <div className="grid grid-cols-3 gap-2 mt-2">
      {months.map((m, i) => {
        const isSelected =
          selected &&
          selected.getFullYear() === currentYearMonthView[0] &&
          selected.getMonth() === i;
        return (
          <Button
            key={m}
            onClick={() => {
              setCurrentYearMonthView((prev) => [prev[0], i]);
              setViewMode("date");
            }}
            variant={"ghost"}
            className={cn(
              isSelected
                ? "bg-primary text-primary-foreground hover:bg-primary hover:text-primary-foreground dark:hover:bg-primary dark:hover:text-primary-foreground"
                : ""
            )}
          >
            {m}
          </Button>
        );
      })}
    </div>
  );

  const renderYearSelector = () => {
    const years = Array.from(
      { length: 25 },
      (_, i) => currentYearMonthView[0] - 12 + i
    );
    return (
      <div className="grid grid-cols-5 gap-2 mt-2">
        {years.map((y) => {
          const isSelected =
            selected &&
            selected.getFullYear() === y &&
            selected.getMonth() === currentYearMonthView[1];
          return (
            <Button
              key={y}
              onClick={() => {
                setCurrentYearMonthView((prev) => [y, prev[1]]);
                setViewMode("month");
              }}
              variant={"ghost"}
              className={cn(
                isSelected
                  ? "bg-primary text-primary-foreground hover:bg-primary hover:text-primary-foreground dark:hover:bg-primary dark:hover:text-primary-foreground"
                  : ""
              )}
            >
              {y}
            </Button>
          );
        })}
      </div>
    );
  };

  return (
    <div className="relative inline-block" ref={pickerRef}>
      <Button onClick={() => setShow(!show)}>
        {new Intl.DateTimeFormat("en-US", {
          dateStyle: "medium",
        }).format(selected || new Date())}
      </Button>

      {show && (
        <Card className={cn("absolute z-10 p-4 border rounded w-64 gap-0")}>
          <div className="flex justify-between items-center mb-2">
            <Button
              variant={"ghost"}
              size={"icon"}
              onClick={() => handleChevronOnClick("left")}
            >
              <ChevronLeft />
            </Button>
            <Button
              variant={"ghost"}
              onClick={() =>
                setViewMode(
                  viewMode === "date"
                    ? "month"
                    : viewMode === "month"
                    ? "year"
                    : "date"
                )
              }
            >
              {viewMode !== "year" && months[currentYearMonthView[1]]}{" "}
              {currentYearMonthView[0]}
            </Button>
            <Button
              variant={"ghost"}
              size={"icon"}
              onClick={() => handleChevronOnClick("right")}
            >
              <ChevronRight />
            </Button>
          </div>

          {viewMode === "date" && renderCalendar()}
          {viewMode === "month" && renderMonthSelector()}
          {viewMode === "year" && renderYearSelector()}
        </Card>
      )}
    </div>
  );
};

export default AppDatePicker;
